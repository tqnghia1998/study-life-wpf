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

namespace Client.Subject
{
    public partial class PageDetailSubject : Page
    {
        List<CheckBox> check = new List<CheckBox>();
        List<ComboBox> editStartTime = new List<ComboBox>();
        List<ComboBox> editFinishTime = new List<ComboBox>();
        List<TextBox> editRoom = new List<TextBox>();
        ObservableCollection<CFaculty> faculties;
        ObservableCollection<CTerm> terms;
        ObservableCollection<CSchedule> schedules;
        CSubject subject;

        // Delegate để làm mới danh sách môn học
        public delegate void DelegateRefeshSubjectList();
        public DelegateRefeshSubjectList RefreshSubjectList;

        public PageDetailSubject(CSubject _subject)
        {
            InitializeComponent();
            subject = _subject;

            check.Add(checkMonday);
            check.Add(checkTuesday);
            check.Add(checkWednesday);
            check.Add(checkThursday);
            check.Add(checkFriday);
            check.Add(checkSaturday);
            editStartTime.Add(editStartTimeMonday);
            editStartTime.Add(editStartTimeTuesday);
            editStartTime.Add(editStartTimeWednesday);
            editStartTime.Add(editStartTimeThursday);
            editStartTime.Add(editStartTimeFriday);
            editStartTime.Add(editStartTimeSaturday);
            editFinishTime.Add(editFinishTimeMonday);
            editFinishTime.Add(editFinishTimeTuesday);
            editFinishTime.Add(editFinishTimeWednesday);
            editFinishTime.Add(editFinishTimeThursday);
            editFinishTime.Add(editFinishTimeFriday);
            editFinishTime.Add(editFinishTimeSaturday);
            editRoom.Add(editRoomMonday);
            editRoom.Add(editRoomTuesday);
            editRoom.Add(editRoomWednesday);
            editRoom.Add(editRoomThursday);
            editRoom.Add(editRoomFriday);
            editRoom.Add(editRoomSaturday);
            for (int i = 6; i <= 18; i++)
            {
                for (int j = 0; j <= 5; j++)
                {
                    for (int k = 0; k < editStartTime.Count; k++)
                    {
                        editStartTime[k].Items.Add(i.ToString("00") + ":" + (j * 10).ToString("00"));
                        editFinishTime[k].Items.Add(i.ToString("00") + ":" + (j * 10).ToString("00"));
                    }
                }
            }

            new Thread(() =>
            {
                try
                {
                    // Lấy JSON các khoa về
                    HttpRequest http = new HttpRequest();
                    http.Cookies = MainWindow.cookies;
                    string httpResponse = http.Get(MainWindow.domainURL + "/faculties").ToString();

                    if (httpResponse.Equals("Đã hết phiên hoạt động"))
                    {
                        new Dialog(Window.GetWindow(this), httpResponse).ShowDialog();
                    }
                    else
                    {
                        // Parse thành các đối tượng CFaculty
                        faculties = JsonConvert.DeserializeObject<ObservableCollection<CFaculty>>(httpResponse);
                        
                        Dispatcher.Invoke(() => {
                            editFaculty.ItemsSource = faculties;
                        });
                    }

                    // Lấy JSON các học kỳ về
                    HttpRequest httpTerm = new HttpRequest();
                    httpTerm.Cookies = MainWindow.cookies;
                    string httpTermResponse = httpTerm.Get(MainWindow.domainURL + "/terms").ToString();

                    if (httpTermResponse.Equals("Đã hết phiên hoạt động"))
                    {
                        new Dialog(Window.GetWindow(this), httpTermResponse).ShowDialog();
                    }
                    else
                    {
                        // Parse thành các đối tượng CTerm
                        terms = JsonConvert.DeserializeObject<ObservableCollection<CTerm>>(httpTermResponse);

                        // Thêm vào Combobox
                        for (int i = 0; i < terms.Count; i++)
                        {
                            if (editTermYear.Items.Contains(terms[i].termyear) == false)
                            {
                                Dispatcher.Invoke(() =>
                                {
                                    editTermYear.Items.Add(terms[i].termyear);
                                });
                            }
                        }
                    }

                    // Nếu user đang muốn xem chi tiết một môn học thì hiển thị thông tin
                    if (subject != null)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            editSubjectId.Text = subject.subjectid;
                            editSubjectName.Text = subject.subjectname;
                            editTeacherName.Text = subject.teachername;
                            editCredit.Text = subject.credit.ToString();
                            editTermYear.Text = subject.termyear;
                            editTermIndex.ItemsSource = terms.Where(x => x.termyear.Equals(subject.termyear));
                            editTermIndex.Text = subject.termindex;
                            editFaculty.SelectedItem = faculties.FirstOrDefault(x => x.facultyid.Equals(subject.faculty));
                            editSubjectId.IsEnabled = false;
                        });

                        // Lấy JSON các lịch học về
                        HttpRequest httpSchedule = new HttpRequest();
                        httpSchedule.Cookies = MainWindow.cookies;
                        string httpScheduleRespone = httpSchedule.Get(MainWindow.domainURL + "/schedules/" + subject.subjectid).ToString();

                        // Parse thành các đối tượng CSchedule
                        schedules = JsonConvert.DeserializeObject<ObservableCollection<CSchedule>>(httpScheduleRespone);

                        for (int i = 0; i < check.Count; i++)
                        {
                            var checkContent = string.Empty;
                            Dispatcher.Invoke(() =>
                            {
                                checkContent = check[i].Content.ToString();
                            });

                            var schedule = schedules.FirstOrDefault(x => x.day.Equals(checkContent));
                            if (schedule != null)
                            {
                                Dispatcher.Invoke(() =>
                                {
                                    check[i].IsChecked = true;
                                    editRoom[i].Text = schedule.room;
                                    editStartTime[i].Text = schedule.starttime.Substring(0, 5);
                                    editFinishTime[i].Text = schedule.finishtime.Substring(0, 5);
                                });
                            }
                        }
                    }
                }
                catch (Exception e) { }
            }).Start();
        }

        /// <summary>
        /// Hiệu ứng khi chọn Combobox
        /// </summary>
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
            if ((sender as ComboBox) == editTermYear)
            {
                var curTermYear = editTermYear.Text;
                var listTermSameYear = terms.Where(x => x.termyear.Equals(curTermYear));
                editTermIndex.ItemsSource = listTermSameYear;
            }
        }

        /// <summary>
        /// Thêm bài tập mới
        /// </summary>
        private void BtnAddSubject_Click(object sender, RoutedEventArgs e)
        {
            // Pha kiểm tra dữ hợp lệ
            if (editSubjectId.Text.Length == 0 || editSubjectName.Text.Length == 0 || editTeacherName.Text.Length == 0
                || editCredit.Text.Length == 0 || editTermIndex.Text.Length == 0 || editTermYear.Text.Length == 0 || editFaculty.Text.Length == 0)
            {
                var dialog = new Dialog(Window.GetWindow(this), "Vui lòng nhập đầy đủ thông tin");
                dialog.ShowDialog();
                return;
            }
            else
            {
                for (int i = 0; i < editStartTime.Count; i++)
                {
                    if (check[i].IsChecked == true)
                    {
                        if (editStartTime[i].Text.Length == 0 || editFinishTime[i].Text.Length == 0 || editRoom[i].Text.Length == 0)
                        {
                            var dialog = new Dialog(Window.GetWindow(this), "Vui lòng nhập đầy đủ thông tin");
                            dialog.ShowDialog();
                            return;
                        }
                        else
                        {
                            if (string.Compare(editFinishTime[i].Text, editStartTime[i].Text) <= 0)
                            {
                                var dialog = new Dialog(Window.GetWindow(this), "Giờ bắt đầu phải trước giờ kết thúc");
                                dialog.ShowDialog();
                                return;
                            }
                        }
                    }
                }
            }

            // Gửi dữ liệu lên Server
            try
            {
                if (subject == null)
                {
                    addSubject();
                }
                else
                {
                    editSubject();
                }
            }
            catch (Exception) { }
            
            // Quay về trang trước (và cập nhật danh sách)
            if (RefreshSubjectList != null)
            {
                RefreshSubjectList.Invoke();
            }
            NavigationService.GoBack();
        }

        /// <summary>
        /// Gửi yêu cầu tạo môn học mới
        /// </summary>
        private void addSubject()
        {
            // Thông báo xác nhận
            var dialogConfirm = new Dialog(Window.GetWindow(this), "Xác nhận tạo môn học?");
            if (dialogConfirm.ShowDialog() == false) return;

            // Tạo môn học trước
            subject = new CSubject();
            subject.subjectid = editSubjectId.Text;
            subject.subjectname = editSubjectName.Text;
            subject.teachername = editTeacherName.Text;
            subject.credit = int.Parse(editCredit.Text);
            subject.termindex = editTermIndex.Text;
            subject.termyear = editTermYear.Text;
            subject.faculty = (editFaculty.SelectedItem as CFaculty).facultyid;

            // Gửi môn học mới lên Server
            var request = new HttpRequest();
            request.Cookies = MainWindow.cookies;
            request.AddParam("subjectid", subject.subjectid);
            request.AddParam("subjectname", subject.subjectname);
            request.AddParam("teachername", subject.teachername);
            request.AddParam("credit", subject.credit);
            request.AddParam("termindex", subject.termindex);
            request.AddParam("termyear", subject.termyear);
            request.AddParam("faculty", subject.faculty);

            HttpResponse addSubjectResult = request.Raw(HttpMethod.POST, MainWindow.domainURL + "/subjects");
            new Dialog(Window.GetWindow(this), addSubjectResult.ToString()).ShowDialog();
            if (addSubjectResult.StatusCode != xNet.HttpStatusCode.OK) return;

            // Gửi danh sách lịch học lên Server
            for (int i = 0; i < check.Count; i++)
            {
                if (check[i].IsChecked == true)
                {
                    var scheduleRequest = new HttpRequest();
                    scheduleRequest.Cookies = MainWindow.cookies;
                    scheduleRequest.AddParam("subjectid", subject.subjectid);
                    scheduleRequest.AddParam("day", check[i].Content);
                    scheduleRequest.AddParam("room", editRoom[i].Text);
                    scheduleRequest.AddParam("startTime", editStartTime[i].Text);
                    scheduleRequest.AddParam("finishTime", editFinishTime[i].Text);

                    HttpResponse addScheduleResult = scheduleRequest.Raw(HttpMethod.POST, MainWindow.domainURL + "/schedules");
                    if (addScheduleResult.StatusCode != xNet.HttpStatusCode.OK)
                    {
                        new Dialog(Window.GetWindow(this), addScheduleResult.ToString()).ShowDialog();
                    }
                }
            }
        }
        
        /// <summary>
        /// Gửi yêu cầu sửa môn học
        /// </summary>
        private void editSubject()
        {
            // Thông báo xác nhận
            var dialogConfirm = new Dialog(Window.GetWindow(this), "Xác nhận sửa môn học?");
            if (dialogConfirm.ShowDialog() == false) return;

            // Tạo môn học trước
            subject.subjectid = editSubjectId.Text;
            subject.subjectname = editSubjectName.Text;
            subject.teachername = editTeacherName.Text;
            subject.credit = int.Parse(editCredit.Text);
            subject.termindex = editTermIndex.Text;
            subject.termyear = editTermYear.Text;
            subject.faculty = (editFaculty.SelectedItem as CFaculty).facultyid;

            // Gửi cập nhật môn học lên Server
            var request = new HttpRequest();
            request.Cookies = MainWindow.cookies;
            request.AddParam("subjectid", subject.subjectid);
            request.AddParam("subjectname", subject.subjectname);
            request.AddParam("teachername", subject.teachername);
            request.AddParam("credit", subject.credit);
            request.AddParam("termindex", subject.termindex);
            request.AddParam("termyear", subject.termyear);
            request.AddParam("faculty", subject.faculty);

            HttpResponse editSubjectResult = request.Raw(HttpMethod.PUT, MainWindow.domainURL + "/subjects/" + subject.subjectid);
            new Dialog(Window.GetWindow(this), editSubjectResult.ToString()).ShowDialog();
            if (editSubjectResult.StatusCode != xNet.HttpStatusCode.OK) return;

            // Gửi cập nhật lịch học lên Server
            for (int i = 0; i < check.Count; i++)
            {
                var schedule = schedules.FirstOrDefault(x => x.day.Equals(check[i].Content.ToString()));

                // Trường hợp ngày này có check
                if (check[i].IsChecked == true)
                {
                    // Nhưng trong dữ liệu cũ không có => Thêm mới
                    if (schedule == null)
                    {
                        var requestAdd = new HttpRequest();
                        requestAdd.Cookies = MainWindow.cookies;
                        requestAdd.AddParam("subjectid", subject.subjectid);
                        requestAdd.AddParam("day", check[i].Content.ToString());
                        requestAdd.AddParam("room", editRoom[i].Text);
                        requestAdd.AddParam("starttime", editStartTime[i].Text.Substring(0, 5));
                        requestAdd.AddParam("finishtime", editFinishTime[i].Text.Substring(0, 5));
                        requestAdd.Raw(HttpMethod.POST, MainWindow.domainURL + "/schedules");
                    }
                    // Ngược lại => Sửa
                    else
                    {
                        var requestEdit = new HttpRequest();
                        requestEdit.Cookies = MainWindow.cookies;
                        requestEdit.AddParam("room", editRoom[i].Text);
                        requestEdit.AddParam("starttime", editStartTime[i].Text.Substring(0, 5));
                        requestEdit.AddParam("finishtime", editFinishTime[i].Text.Substring(0, 5));
                        requestEdit.Raw(HttpMethod.PUT, MainWindow.domainURL + "/schedules/" + schedule.subjectid + "/" + schedule.day);
                    }
                }
                // Trường hợp ngày này không có check
                else
                {
                    // Nhưng trong dữ liệu cũ lại có => Xóa
                    if (schedule != null)
                    {
                        var requestDelete = new HttpRequest();
                        requestDelete.Cookies = MainWindow.cookies;
                        requestDelete.Raw(HttpMethod.DELETE, MainWindow.domainURL + "/schedules/" + schedule.subjectid + "/" + schedule.day);
                    }
                }
            }
        }
    }
}
