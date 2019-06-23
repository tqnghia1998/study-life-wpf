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
    /// Interaction logic for PageEditHomework.xaml
    /// </summary>
    public partial class PageEditHomework : Page
    {
        CTask task;
        ObservableCollection<CSubject> subjects;
        public delegate void RefreshPageTask();
        public event RefreshPageTask Refresh = null;
        public PageEditHomework()
        {
            InitializeComponent();
        }

        public PageEditHomework(CTask ctask)
        {
            InitializeComponent();
            task = ctask;
            this.DataContext = task;
            this.taskid.IsEnabled = false;

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
                        var selectedindex = 0;
                        if (task.subjectid != null)
                        {
                            for (int i = 0; i < subjects.Count; i++)
                            {
                                if (task.subjectname == subjects[i].subjectname)
                                {
                                    selectedindex = i;
                                    break;
                                }
                            }
                        } else
                        {
                            selectedindex = subjects.Count - 1;
                        }

                        // Thêm vào Combobox
                        Dispatcher.Invoke(() =>
                        {
                            subjectname.ItemsSource = subjects;
                            subjectname.SelectedIndex = selectedindex;
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


        private void UpdateTask_Click(object sender, RoutedEventArgs e)
        {
            // Tạo đối tượng từ các TextBox
            task.taskname = taskname.Text.Length == 0 ? null : taskname.Text;
            task.taskid = taskid.Text.Length == 0 ? null : taskid.Text;

            task.subjectid = (subjectname.SelectedValue as CSubject).subjectid;

            task.progress = (int)progress.Value;
            task.description = description.Text.Length == 0 ? null : description.Text;
            task.deadline = (DateTime)deadline.SelectedDate;
            
            // Tham số cần truyền
            var request = new HttpRequest();
            request.Cookies = MainWindow.cookies;
            request.AddParam("taskname", task.taskname);
            request.AddParam("taskid", task.taskid);
            request.AddParam("subjectid", task.subjectid);
            request.AddParam("progress", task.progress);
            request.AddParam("description", task.description);
            request.AddParam("deadline", task.deadline.ToString("yyyy/MM/dd HH:mm:ss"));

            HttpResponse updateTask = request.Raw(HttpMethod.PUT, MainWindow.domainURL + "/tasks/" + task.taskid);
            new Dialog(Window.GetWindow(this), updateTask.ToString()).ShowDialog();
            if (updateTask.StatusCode != xNet.HttpStatusCode.OK) return;

            if (Refresh != null)
            {
                Refresh();
            }
            // Gọi lại trang danh sách task
            NavigationService.GoBack();
        }

        private void CancelUpdateTask_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
