using Client.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using xNet;

namespace Client.HomeWork
{
    /// <summary>
    /// Interaction logic for PageHomework.xaml
    /// </summary>
    public partial class PageHomework : Page
    {

        ObservableCollection<CTask> tasks;
        ObservableCollection<CSubject> subjects;
        public PageHomework()
        {
            InitializeComponent();
            refreshPage();

        }

        public void refreshPage()
        {
            new Thread(() =>
            {
                try
                {
                    // Lấy JSON các khoa về
                    HttpRequest http = new HttpRequest();
                    http.Cookies = MainWindow.cookies;
                    string httpResponseTask = http.Get(MainWindow.domainURL + "/tasks").ToString();
                    string httpResponseSubject = http.Get(MainWindow.domainURL + "/subjects").ToString();
                    if (httpResponseTask.Equals("Đã hết phiên hoạt động"))
                    {
                        new Dialog(Window.GetWindow(this), httpResponseTask).ShowDialog();
                    }
                    else
                    {
                        // Parse thành các đối tượng task
                        tasks = JsonConvert.DeserializeObject<ObservableCollection<CTask>>(httpResponseTask);
                        subjects = JsonConvert.DeserializeObject<ObservableCollection<CSubject>>(httpResponseSubject);
                        subjects.Insert(0, new CSubject
                        {
                            subjectid = "",
                            subjectname = "Khác"
                        });
                        // Thêm vào Combobox
                        Dispatcher.Invoke(() =>
                        {
                            spListTask.ItemsSource = tasks;
                            subjectname.ItemsSource = subjects;
                            subjectname.SelectedIndex = 0;
                        });

                    }
                }
                catch (Exception) { }
            }).Start();
        }

        CTask currentTask = new CTask();
        private void DataGridCell_PreviewMouseLeftButtonDownProduct(object sender, MouseButtonEventArgs e)
        {
            DataGridCell myCell = sender as DataGridCell;
            DataGridRow row = DataGridRow.GetRowContainingElement(myCell);
            CTask temp = row.DataContext as CTask;
            currentTask = temp;
        }

        private void ProgressSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageEditHomework(currentTask));
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAddHomework());
        }

        private void SearchKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<CTask> taskSearch = new ObservableCollection<CTask>(tasks);
            for (int i = 0; i < taskSearch.Count; i++)
            {
                if (!((taskSearch[i].taskid.Contains(searchKey.Text)
                    || taskSearch[i].taskname.Contains(searchKey.Text))
                    && taskSearch[i].subjectid == subjectidselected))
                {
                    taskSearch.RemoveAt(i);
                    i--;
                }
            }

            // Thêm vào Combobox
            Dispatcher.Invoke(() =>
            {
                spListTask.ItemsSource = taskSearch;
            });
        }

        string subjectidselected;
        private void Subjectname_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            subjectidselected = (subjectname.SelectedItem as CSubject).subjectid;
            ObservableCollection<CTask> taskSearch = new ObservableCollection<CTask>(tasks);
            if (subjectidselected != "")
            {
                for (int i = 0; i < taskSearch.Count; i++)
                {
                    if (!(taskSearch[i].subjectid == subjectidselected))
                    {
                        taskSearch.RemoveAt(i);
                        i--;
                    }
                }
            }

            // Thêm vào Combobox
            Dispatcher.Invoke(() =>
            {
                spListTask.ItemsSource = taskSearch;
            });
        }


        private void Combobox_DropDownOpened(object sender, EventArgs e)
        {
            (sender as ComboBox).Background = Brushes.LightGray;
        }

        /// <summary>
        /// Hiệu ứng khi bỏ chọn Combobox
        /// </summary>
        private void Combobox_DropDownClosed(object sender, EventArgs e)
        {
            (sender as ComboBox).Background = Brushes.Transparent;
        }

        //#region Cập nhật tiến độ trực tiếp - Chưa làm được
        //ListBox listbox;
        //private void ProgressSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    foreach (var item in listbox.Items)
        //    {
        //        var container = listbox.ItemContainerGenerator.ContainerFromItem(item);
        //        var children = AllChildren(container);
        //        var name = "progressSlider";
        //        var control = (Slider)children.First(c => c.Name == name);
        //    }
        //}

        //public List<Control> AllChildren(DependencyObject parent)
        //{
        //    var list = new List<Control> { };
        //    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        //    {
        //        var child = VisualTreeHelper.GetChild(parent, i);
        //        if (child is Control)
        //            list.Add(child as Control);
        //        list.AddRange(AllChildren(child));
        //    }
        //    return list;
        //}

        //private void SpListTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}
        //#endregion

        //CTask currentTask = new CTask();
        //private void SpListTask_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    currentTask = ((System.Windows.FrameworkElement)e.OriginalSource).DataContext as CTask;
        //}

        //private void EditTaskButton_Click(object sender, RoutedEventArgs e)
        //{
        //    NavigationService.Navigate(new PageEditHomework(currentTask));
        //}

        //private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
