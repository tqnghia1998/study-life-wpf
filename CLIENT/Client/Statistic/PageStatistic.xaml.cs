using Syncfusion.UI.Xaml.Schedule;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

namespace Client.Statistic
{
    public partial class PageStatistic : Page
    {
        #region Properties
        private ObservableCollection<DateTime> MonthlyOccurrance = new ObservableCollection<DateTime>();
        private ObservableCollection<string> MonthlyOccurranceSubjects = new ObservableCollection<string>() { "Pay House Rent", "Car Service", "Medical Check Up" };
        #endregion
        public bool AllowAppointmentDrag { get; set; }

        public PageStatistic()
        {
            InitializeComponent();
            AllowAppointmentDrag = false;

            // Display vietnamese language
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("vi-vn");
            CultureInfo culture = CultureInfo.CreateSpecificCulture("vi-vn");
            Thread.CurrentThread.CurrentCulture = culture;

            // List schedules
            ScheduleAppointmentCollection AppointmentCollection = new ScheduleAppointmentCollection();
            Schedule.Appointments = AppointmentCollection;
            
            // Get data from server
            new Thread(() =>
            {
                try
                {
                    // Lấy JSON các khoa về
                    HttpRequest http = new HttpRequest();
                    http.Cookies = MainWindow.cookies;
                    string httpResponse = http.Get(MainWindow.domainURL + "/registers/" + MainWindow.userid).ToString();

                    if (httpResponse.Equals("Đã hết phiên hoạt động"))
                    {
                        new Dialog(Window.GetWindow(this), httpResponse).ShowDialog();
                    }
                    else
                    {
                        // Parse thành các đối tượng CFaculty
                        //faculties = JsonConvert.DeserializeObject<ObservableCollection<CFaculty>>(httpResponse);

                        //// Thêm vào Combobox
                        //Dispatcher.Invoke(() => {
                        //    listFaculty.ItemsSource = faculties;
                        //    ProgressBar.Visibility = Visibility.Hidden;
                        //});

                        //// Thêm vào danh sách tên khoa
                        //foreach (CFaculty faculty in faculties)
                        //{
                        //    listFacultyName.Add(faculty.facultyname);
                        //};
                    }
                }
                catch (Exception) { }
            }).Start();

            // Create a appoinment
            //var appointment = new ScheduleAppointment()
            //{
            //    StartTime = DateTime.Now.AddHours(-8).AddMinutes(15),
            //    EndTime = DateTime.Now.AddHours(-6),
            //    AppointmentBackground = new SolidColorBrush(Color.FromArgb(0xFF, 0xD8, 0x00, 0x73)),
            //    Subject = "Họp ban cán sự lớp 12A1\nE404",
            //    Notes = "Giáo viên",
            //    Location = "Phòng 304"
            //};


            // AppointmentCollection.Add(appointment);
        }

        /// <summary>
        /// Change display type
        /// </summary>
        private void Btn_ScheduleType_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as RadioButton).Name)
            {
                case "Week":
                {
                    Schedule.ScheduleType = ScheduleType.Week;
                    break;
                }
                case "Month":
                {
                    Schedule.ScheduleType = ScheduleType.Month;
                    break;
                }
            }
        }
        
        /// <summary>
        /// Disable drag and drop
        /// </summary>
        private void Schedule_AppointmentDragging(object sender, AppointmentDraggingEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// Disable context menu
        /// </summary>
        private void Schedule_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// Open detail dialog
        /// </summary>
        private void Schedule_AppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            e.Cancel = true;
            var app = e.Appointment as ScheduleAppointment;
            new Dialog(Window.GetWindow(this), app.Notes).ShowDialog();
        }
    }
}
