using Client.Classes;
using Client.Custom;
using Newtonsoft.Json;
using Syncfusion.UI.Xaml.Schedule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using xNet;

namespace Client.Statistic
{
    public partial class PageStatistic : Page
    {
        ObservableCollection<CRegister> registers;
        ScheduleAppointmentCollection appointmentCollection = new ScheduleAppointmentCollection();
        SolidColorBrush[] listColor = new SolidColorBrush[]
        {
            new SolidColorBrush(Colors.MediumOrchid),
            new SolidColorBrush(Colors.ForestGreen),
            new SolidColorBrush(Colors.DodgerBlue),
            new SolidColorBrush(Colors.DarkOrange),
            new SolidColorBrush(Colors.Teal),
            new SolidColorBrush(Colors.YellowGreen),
            new SolidColorBrush(Colors.Firebrick),
            new SolidColorBrush(Colors.MediumBlue),
            new SolidColorBrush(Colors.SlateGray),
            new SolidColorBrush(Colors.BlueViolet),
            new SolidColorBrush(Colors.DarkSlateBlue),
            new SolidColorBrush(Colors.DeepPink)
        };

        public bool AllowAppointmentDrag { get; set; }

        public PageStatistic()
        {
            InitializeComponent();
            AllowAppointmentDrag = false;

            // Display vietnamese language
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi-vn");
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
        }

        /// <summary>
        /// Hiển thị danh sách lịch học
        /// </summary>
        private void showSchedules()
        {
            appointmentCollection.Clear();
            var listSubject = new List<string>();
            for (int i = 0; i < registers.Count; i++)
            {
                int colorIndex = -1;
                var register = registers[i];
                if (!listSubject.Contains(register.subjectid))
                {
                    listSubject.Add(register.subjectid);
                    colorIndex = (listSubject.Count - 1) % listColor.Length;
                }
                else colorIndex = listSubject.FindIndex(x => x.Equals(register.subjectid)) % listColor.Length;

                // Xác định ngày học đầu tiên trong kỳ
                var firstLearntDayOfWeek = register.GetFirstDayOfWeek();
                var learntDayOfWeek = register.GetLearntDayOfWeek();
                var firstLearntDay = register.begindate;
                while (firstLearntDayOfWeek != learntDayOfWeek)
                {
                    firstLearntDayOfWeek = firstLearntDayOfWeek == DayOfWeek.Saturday ? DayOfWeek.Sunday : (firstLearntDayOfWeek + 1);
                    firstLearntDay = firstLearntDay.AddDays(+1);
                }

                // Đặt lịch học vào ngày đó, và các ngày tiếp theo ở các tuần tiếp theo
                for (DateTime dayOfWeek = firstLearntDay; dayOfWeek < register.enddate; dayOfWeek = dayOfWeek.AddDays(7))
                {
                    var schedule = new ScheduleAppointment()
                    {
                        StartTime = DateTime.Parse(dayOfWeek.ToShortDateString() + " " + register.starttime),
                        EndTime = DateTime.Parse(dayOfWeek.ToShortDateString() + " " + register.finishtime),
                        AppointmentBackground = listColor[colorIndex],
                        Subject = register.subjectname,
                        Location = i.ToString()
                    };
                    appointmentCollection.Add(schedule);
                }
            }
            ProgressBar.Visibility = Visibility.Hidden;
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
            int index;
            if (app.Location == null) return;
            int.TryParse(app.Location, out index);
            var infoDialog = new ScheduleInfo(Window.GetWindow(this), registers[index]);
            infoDialog.RefreshScheduleList += showSchedules;
            infoDialog.ShowDialog();
        }
    }
}
