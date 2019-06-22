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

namespace Client.Numbers
{
    public partial class PageNumbers : Page
    {
        /// <summary>
        /// ViewModel cho biểu đồ
        /// </summary>
        public class SubjectDensity
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int Amount { get; set; }
            public int Density { get; set; }
        }

        public PageNumbers()
        {
            InitializeComponent();

            // Lấy danh sách các khoa về
            new Thread(() =>
            {
                try
                {
                    // Lấy JSON các khoa về
                    string response = new HttpRequest().Get(MainWindow.domainURL + "/faculties").ToString();

                    // Parse thành các đối tượng CFaculty
                    var faculties = JsonConvert.DeserializeObject<List<CFaculty>>(response);

                    // Thêm vào Combobox
                    Dispatcher.Invoke(() => {
                        comboFaculty.ItemsSource = faculties;
                        if (faculties.Count > 0)
                        {
                            comboFaculty.SelectedIndex = 0;
                        }
                    });
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }).Start();
            refreshStatistic();
        }

        /// <summary>
        /// Làm mới các biểu đồ
        /// </summary>
        public void refreshStatistic()
        {
            // Sau đó chạy nền lấy dữ liệu và hiển thị
            Thread thread = new Thread(delegate ()
            {
                // Lấy JSON thông tin về
                HttpRequest http = new HttpRequest();
                http.Cookies = MainWindow.cookies;
                string httpResponse = http.Get(MainWindow.domainURL + "/statistics/subjectdensities").ToString();

                if (httpResponse.Equals("Đã hết phiên hoạt động"))
                {
                    new Dialog(Window.GetWindow(this), httpResponse).ShowDialog();
                    return;
                }
                var densities = new ObservableCollection<SubjectDensity>();
                densities = JsonConvert.DeserializeObject<ObservableCollection<SubjectDensity>>(httpResponse);

                // Tính tổng số môn học
                int totalSubject = 0;
                try
                {
                    totalSubject = densities.Sum(x => x.Amount);
                }
                catch (Exception) { }
                
                // Tính tỷ trọng từng khoa
                for (int i = 0; i < densities.Count; i++)
                {
                    densities[i].Density = 100 * densities[i].Amount / totalSubject;
                }

                // Đưa lên UI
                Dispatcher.Invoke(() =>
                {
                    pieChart1.ItemsSource = densities;
                    ProgressBar.IsEnabled = false;
                    ProgressBar.Visibility = Visibility.Hidden;
                });
            });
            thread.Start();
        }

        public void refreshFacultyStatistic()
        {
            // Sau đó chạy nền lấy dữ liệu và hiển thị
            Thread _thread = new Thread(delegate ()
            {
                // Lấy JSON thông tin về
                HttpRequest http = new HttpRequest();
                http.Cookies = MainWindow.cookies;
                var facultyid = string.Empty;
                Dispatcher.Invoke(() =>
                {
                    facultyid = (comboFaculty.SelectedItem as CFaculty).facultyid;
                });
                string httpResponse = http.Get(MainWindow.domainURL + "/statistics/registernumbers/" + facultyid).ToString();

                if (httpResponse.Equals("Đã hết phiên hoạt động"))
                {
                    new Dialog(Window.GetWindow(this), httpResponse).ShowDialog();
                    return;
                }
                var numbers = new ObservableCollection<SubjectDensity>();
                numbers = JsonConvert.DeserializeObject<ObservableCollection<SubjectDensity>>(httpResponse);

                // Đưa lên UI
                Dispatcher.Invoke(() =>
                {
                    columnChart.ItemsSource = numbers;
                });
            });
            _thread.Start();
        }

        /// <summary>
        /// Hiệu ứng khi chọn Combobox
        /// </summary>
        private void ComboFaculty_DropDownOpened(object sender, EventArgs e)
        {
            (sender as ComboBox).Background = Brushes.LightGray;
        }

        /// <summary>
        /// Hiệu ứng khi bỏ chọn Combobox
        /// </summary>
        private void ComboFaculty_DropDownClosed(object sender, EventArgs e)
        {
            (sender as ComboBox).Background = Brushes.Transparent;
        }

        /// <summary>
        /// Sự kiện chọn khoa
        /// </summary>
        private void ComboFaculty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            refreshFacultyStatistic();
        }
    }
}
