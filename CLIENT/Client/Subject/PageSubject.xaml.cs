using Client.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using xNet;

namespace Client.Subject
{
    public partial class PageSubject : Page
    {
        // Danh sách môn học và danh sách tên môn học
        ObservableCollection<CSubject> subjects;
        public ObservableCollection<string> listSubjectName { get; set; }

        public PageSubject()
        {
            InitializeComponent();
            listSubjectName = new ObservableCollection<string>();

            new Thread(() =>
            {
                try
                {
                    // Lấy JSON các khoa về
                    HttpRequest http = new HttpRequest();
                    http.Cookies = MainWindow.cookies;
                    string httpResponse = http.Get(MainWindow.domainURL + "/subjects").ToString();

                    if (httpResponse.Equals("Đã hết phiên hoạt động"))
                    {
                        new Dialog(Window.GetWindow(this), httpResponse).ShowDialog();
                    }
                    else
                    {
                        // Parse thành các đối tượng CFaculty
                        subjects = JsonConvert.DeserializeObject<ObservableCollection<CSubject>>(httpResponse);

                        // Thêm vào Combobox
                        Dispatcher.Invoke(() => {
                            listSubject.ItemsSource = subjects;
                            ProgressBar.Visibility = Visibility.Hidden;
                        });

                        // Thêm vào danh sách tên khoa
                        foreach (CSubject subject in subjects)
                        {
                            listSubjectName.Add(subject.subjectname);
                        };
                    }
                }
                catch (Exception) { }
            }).Start();

            // Thiết lập thanh tìm kiếm
            searchBar.DataContext = this;
        }

        /// <summary>
        /// Tự động điều chỉnh kích thước cột
        /// </summary>
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Window win = Window.GetWindow(this);
            nameSubject.Width = win.Width - 500;
        }

        /// <summary>
        /// Click một môn
        /// </summary>
        private void ListSubject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Lấy đối tượng CSubject tương ứng
            if (listSubject.SelectedItem == null) return;
            CSubject subject = listSubject.SelectedItem as CSubject;

            // Đưa thông tin vào các TextBox
            editTeacherName.Text = subject.teachername;
            editTermIndex.Text = subject.termindex;
            editTermYear.Text = subject.termyear;
            editFaculty.Text = subject.faculty;

            // Bật 2 button Sửa và Xem
            btnSeeStatistic.IsEnabled = true;
            btnUpdateSubject.IsEnabled = true;
        }

        /// <summary>
        /// Thêm một môn học
        /// </summary>
        private void BtnAddSubject_Click(object sender, RoutedEventArgs e)
        {
            var pageDetailSubject = new PageDetailSubject(null);
            pageDetailSubject.RefreshSubjectList = Refresh;
            NavigationService.Navigate(pageDetailSubject);
        }

        /// <summary>
        /// Làm mới danh sách
        /// </summary>
        private void Refresh()
        {
            new Thread(() =>
            {
                try
                {
                    // Lấy JSON các môn học về
                    HttpRequest http = new HttpRequest();
                    http.Cookies = MainWindow.cookies;
                    string httpResponse = http.Get(MainWindow.domainURL + "/subjects").ToString();

                    if (httpResponse.Equals("Đã hết phiên hoạt động"))
                    {
                        new Dialog(Window.GetWindow(this), httpResponse).ShowDialog();
                    }
                    else
                    {
                        // Parse thành các đối tượng CFaculty
                        subjects = JsonConvert.DeserializeObject<ObservableCollection<CSubject>>(httpResponse);

                        // Thêm vào Combobox
                        Dispatcher.Invoke(() => {
                            listSubject.ItemsSource = subjects;
                        });

                        // Thêm vào danh sách tên môn học
                        listSubjectName.Clear();
                        foreach (CSubject subject in subjects)
                        {
                            listSubjectName.Add(subject.subjectname);
                        };
                    }
                }
                catch (Exception) { }
            }).Start();

            btnSeeStatistic.IsEnabled = false;
            btnUpdateSubject.IsEnabled = false;
        }

        /// <summary>
        /// Xem và sửa một môn học
        /// </summary>
        private void BtnUpdateSubject_Click(object sender, RoutedEventArgs e)
        {
            var subject = listSubject.SelectedItem as CSubject;
            var pageDetailSubject = new PageDetailSubject(subject);
            pageDetailSubject.RefreshSubjectList = Refresh;
            NavigationService.Navigate(pageDetailSubject);
        }

        /// <summary>
        /// Tìm kiếm theo tên
        /// </summary>
        private void SearchBar_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (searchBar.Text == string.Empty)
                {
                    if (listSubject.Items.Count > 0)
                    {
                        listSubject.SelectedIndex = 0;
                        listSubject.ScrollIntoView(listSubject.Items[0]);
                    }
                    return;
                }
                editTeacherName.Clear();
                editTermIndex.Clear();
                editTermYear.Clear();
                editFaculty.Clear();
                foreach (CSubject subject in subjects)
                {
                    if (subject.subjectname.ToUpper().Contains(searchBar.Text.ToUpper()))
                    {
                        listSubject.SelectedItem = subject;
                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Hàm chuyển đổi STT cho List View
    /// </summary>
    public class DataConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
        {
            ListViewItem item = (ListViewItem)value;
            ListView listView = ItemsControl.ItemsControlFromItemContainer(item) as ListView;
            int index = listView.ItemContainerGenerator.IndexFromContainer(item) + 1;
            return index.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
