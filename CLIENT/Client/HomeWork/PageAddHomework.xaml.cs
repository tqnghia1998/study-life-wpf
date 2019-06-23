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
    /// Interaction logic for PageAddHomework.xaml
    /// </summary>
    public partial class PageAddHomework : Page
    {
        // Danh sách khoa và danh sách tên khoa
        ObservableCollection<CTask> tasks;
        ObservableCollection<CSubject> subjects;
        public delegate void RefreshPageTask();
        public event RefreshPageTask Refresh = null;

        public PageAddHomework()
        {
            InitializeComponent();
            // Đổ dữ liệu xuống combobox
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
                        subjects.Add(new CSubject
                        {
                            subjectid = "",
                            subjectname = "Khác"
                        });
                        // Hiện ra môn học hiện tại của bài tập

                        // Thêm vào Combobox
                        Dispatcher.Invoke(() =>
                        {
                            subjectname.ItemsSource = subjects;
                            subjectname.SelectedIndex = subjects.Count - 1;
                        });

                    }
                }
                catch (Exception) { }
            }).Start();
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

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            CTask task = new CTask()
            {
                taskid = taskid.Text,
                taskname = taskname.Text,
                subjectid = (subjectname.SelectedItem as CSubject).subjectid,
                progress = 0,
                deadline = (DateTime)deadline.SelectedDate,
                description = description.Text,
            };

            // Tham số cần truyền
            var request = new HttpRequest();
            request.Cookies = MainWindow.cookies;
            request.AddParam("taskname", task.taskname);
            request.AddParam("taskid", task.taskid);
            request.AddParam("subjectid", task.subjectid);
            request.AddParam("progress", task.progress);
            request.AddParam("description", task.description);
            request.AddParam("deadline", task.deadline.ToString("yyyy/MM/dd HH:mm:ss"));

            // Gửi dữ liệu lên server
            HttpResponse addTermResult = request.Raw(HttpMethod.POST, MainWindow.domainURL + "/tasks");

            new Dialog(Window.GetWindow(this), addTermResult.ToString()).ShowDialog();
            if (addTermResult.StatusCode != xNet.HttpStatusCode.OK) return;

            if (Refresh != null)
            {
                Refresh();
            }

            NavigationService.GoBack();

        }

        private void CancelAddTask_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
