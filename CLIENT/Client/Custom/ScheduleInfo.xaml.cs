using Client.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using xNet;

namespace Client.Custom
{
    public partial class ScheduleInfo : Window
    {
        CRegister register;

        // Delegate để làm mới lịch học
        public delegate void DelegateRefreshSchedule();
        public DelegateRefreshSchedule RefreshScheduleList;

        public ScheduleInfo(Window father, CRegister _register)
        {
            InitializeComponent();
            register = _register;
            Owner = father;

            // Assign data
            Title.Text = register.subjectname;
            txtSubjectId.Text = ": " + register.subjectid;
            txtCredit.Text = ": " + register.credit;
            txtTeacherName.Text = ": " + register.teachername;
            txtFacultyname.Text = ": " + register.facultyname;
            txtBeginDate.Text = ": " + register.begindate.ToShortDateString();

            txtDayOfWeek.Text = ":" + register.day.Substring(register.day.IndexOf(' '));
            txtRoom.Text = ": " + register.room;
            txtTime.Text = ": " + register.starttime + " - " + register.finishtime;
            txtTerm.Text = ": " + register.termindex + "/" + register.termyear;
            txtEndDate.Text = ": " + register.enddate.ToShortDateString();
        }

        /// <summary>
        /// Vị trí xuất hiện cửa sổ
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Left = Owner.Left + Owner.Width / 2 - Width / 2;
            Top = Owner.Top + Owner.Height / 2 - Height / 2;
        }

        /// <summary>
        /// Tắt cửa sổ
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// Di chuyển cửa sổ
        /// </summary>
        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) DragMove();
        }

        /// <summary>
        /// Hủy đăng ký môn học
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (register.enddate > DateTime.Now.Date)
            {
                var dialog = new Dialog(this, "Xác nhận hủy đăng ký?");
                if (dialog.ShowDialog() == true)
                {
                    var request = new HttpRequest();
                    request.Cookies = MainWindow.cookies;
                    HttpResponse deleteRegisterResult = request.Raw(HttpMethod.DELETE, MainWindow.domainURL + "/registers/" + register.userid + "/" + register.subjectid);
                    new Dialog(this, deleteRegisterResult.ToString()).ShowDialog();
                    if (deleteRegisterResult.StatusCode == HttpStatusCode.OK)
                    {
                        if (RefreshScheduleList != null)
                        {
                            RefreshScheduleList.Invoke();
                        }
                    }
                }
            }
            else
            {
                new Dialog(this, "Môn học đã bắt đầu!").ShowDialog();
            }
        }

        /// <summary>
        /// Thoát
        /// </summary>
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
