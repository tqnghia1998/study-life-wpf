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

        /// <summary>
        /// Lấy item khi nhấn nút chỉnh sửa hoặc xoá
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridCell_PreviewMouseLeftButtonDownProduct(object sender, MouseButtonEventArgs e)
        {
            DataGridCell myCell = sender as DataGridCell;
            DataGridRow row = DataGridRow.GetRowContainingElement(myCell);
            CTask temp = row.DataContext as CTask;
            currentTask = temp;
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            PageEditHomework pageEditHomework = new PageEditHomework(currentTask);
            pageEditHomework.Refresh += refreshPage;
            NavigationService.Navigate(pageEditHomework);
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            PageAddHomework pageAddHomework = new PageAddHomework();
            pageAddHomework.Refresh += refreshPage;
            NavigationService.Navigate(pageAddHomework);
        }

        private void SearchKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<CTask> taskSearch = new ObservableCollection<CTask>(tasks);
            for (int i = 0; i < taskSearch.Count; i++)
            {
                if (!((taskSearch[i].taskid.Contains(searchKey.Text)
                    || taskSearch[i].taskname.Contains(searchKey.Text))))
                {
                    if (subjectidselected != "")
                    {
                        taskSearch.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        if (taskSearch[i].subjectid != subjectidselected)
                        {
                            taskSearch.RemoveAt(i);
                            i--;
                        }
                    }
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

        #region Hiệu ứng Combobox
        private void Combobox_DropDownOpened(object sender, EventArgs e)
        {
            (sender as ComboBox).Background = Brushes.LightGray;
        }

        private void Combobox_DropDownClosed(object sender, EventArgs e)
        {
            (sender as ComboBox).Background = Brushes.Transparent;
        }
        #endregion

        /// <summary>
        /// Cập nhật tiến độ ngay trên View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Slider slider = sender as Slider;
            var request = new HttpRequest();
            request.Cookies = MainWindow.cookies;
            request.AddParam("taskid", currentTask.taskid);
            request.AddParam("progress", slider.Value);

            HttpResponse updateTask = request.Raw(HttpMethod.PUT, MainWindow.domainURL + "/tasks/" + currentTask.taskid);
            new Dialog(Window.GetWindow(this), updateTask.ToString()).ShowDialog();
            if (updateTask.StatusCode != xNet.HttpStatusCode.OK) return;
        }

        private void SortType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sortType.SelectedIndex == 0)
            {
                if(subjectname.SelectedIndex == 0) spListTask.ItemsSource = tasks.OrderBy(s => s.deadline).Reverse().ToList();
                else
                {
                    var tempid = (subjectname.SelectedItem as CSubject).subjectid;
                    spListTask.ItemsSource = tasks.Where(a => a.subjectid == tempid).OrderBy(s => s.deadline).Reverse().ToList();
                }
            } else
            {
                if (subjectname.SelectedIndex == 0) spListTask.ItemsSource = tasks.OrderBy(s => s.progress).Reverse().ToList();
                else
                {
                    var tempid = (subjectname.SelectedItem as CSubject).subjectid;
                    spListTask.ItemsSource = tasks.Where(a => a.subjectid == tempid).OrderBy(s => s.progress).Reverse().ToList();
                }
            }
        }

        private void Deletetask_Click(object sender, RoutedEventArgs e)
        {
            var request = new HttpRequest();
            request.Cookies = MainWindow.cookies;
            request.AddParam("taskid", currentTask.taskid);

            HttpResponse updateTask = request.Raw(HttpMethod.DELETE, MainWindow.domainURL + "/tasks/" + currentTask.taskid);
            new Dialog(Window.GetWindow(this), updateTask.ToString()).ShowDialog();
            if (updateTask.StatusCode != xNet.HttpStatusCode.OK) return;

            refreshPage();
        }
    }
}
