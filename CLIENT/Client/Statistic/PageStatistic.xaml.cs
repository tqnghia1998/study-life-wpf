using Client.Classes;
using Newtonsoft.Json;
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
        ObservableCollection<CRegister> registers;
        ScheduleAppointmentCollection appointmentCollection = new ScheduleAppointmentCollection();

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
            Schedule.Appointments = appointmentCollection;
            
            // Get data from server
            new Thread(() =>
            {
                try
                {
                    // Lấy JSON các lịch học về
                    HttpRequest http = new HttpRequest();
                    http.Cookies = MainWindow.cookies;
                    string httpResponse = http.Get(MainWindow.domainURL + "/registers/" + MainWindow.user.userid).ToString();

                    if (httpResponse.Equals("Đã hết phiên hoạt động"))
                    {
                        new Dialog(Window.GetWindow(this), httpResponse).ShowDialog();
                    }
                    else
                    {
                        // Parse thành các đối tượng CRegister
                        registers = JsonConvert.DeserializeObject<ObservableCollection<CRegister>>(httpResponse);

                        // Hiển thị lên UI
                        Dispatcher.Invoke(() => {
                            showSchedules();
                        });
                    }
                }
                catch (Exception) { }
            }).Start();

            // AppointmentCollection.Add(appointment);
        }

        /// <summary>
        /// Hiển thị danh sách lịch học
        /// </summary>
        private void showSchedules()
        {
            for (int i = 0; i < registers.Count; i++)
            {
                var register = registers[i];

                // Xác định ngày học đầu tiên trong kỳ
                var firstLearntDayOfWeek = register.GetFirstDayOfWeek();
                var learntDayOfWeek = register.GetLearntDayOfWeek();
                var firstLearntDay = register.begindate;
                while (firstLearntDayOfWeek != learntDayOfWeek)
                {
                    firstLearntDayOfWeek = firstLearntDayOfWeek == DayOfWeek.Saturday ? DayOfWeek.Sunday : (firstLearntDayOfWeek + 1);
                    firstLearntDay = firstLearntDay.AddDays(+1);
                }

                for (DateTime dayOfWeek = firstLearntDay; dayOfWeek < register.enddate; dayOfWeek = dayOfWeek.AddDays(7))
                {
                    var schedule = new ScheduleAppointment()
                    {
                        StartTime = DateTime.Parse(dayOfWeek.ToShortDateString() + " " + register.starttime),
                        EndTime = DateTime.Parse(dayOfWeek.ToShortDateString() + " " + register.finishtime),
                        AppointmentBackground = new SolidColorBrush(Color.FromArgb(0xFF, 0xD8, 0x00, 0x73)),
                        Subject = register.subjectname
                    };
                    appointmentCollection.Add(schedule);
                }
            }
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
