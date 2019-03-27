using Client.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using xNet;

namespace Client
{
    public partial class Register : Page
    {
        // Danh sách khoa
        List<CFaculty> faculties;

        public Register()
        {
            InitializeComponent();
            new Thread(() =>
            {
                try
                {
                    // Lấy JSON các khoa về
                    string response = new HttpRequest().Get(MainWindow.domainURL + "/faculties").ToString();

                    // Parse thành các đối tượng CFaculty
                    faculties = JsonConvert.DeserializeObject<List<CFaculty>>(response);
                    var names = new List<string>();
                    foreach (var faculty in faculties) names.Add(faculty.facultyname);

                    // Thêm vào Combobox
                    Dispatcher.Invoke(() => { editFaculty.ItemsSource = names; });
                }
                catch (Exception) {}
            }).Start();
        }

        /// <summary>
        /// Minimize, Restore, Close
        /// </summary>
        private void BtnCommands_Click(object sender, RoutedEventArgs e)
        {
            Button curButton = sender as Button;
            if (curButton.Tag.Equals("btnClose")) Window.GetWindow(this).Close();
            else if (curButton.Tag.Equals("btnMinim")) Window.GetWindow(this).WindowState = WindowState.Minimized;
            else if (curButton.Tag.Equals("btnMaxim")) Window.GetWindow(this).WindowState =
                    Window.GetWindow(this).WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        /// <summary>
        /// Di chuyển cửa sổ
        /// </summary>
        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) Window.GetWindow(this).DragMove();
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
        }

        /// <summary>
        /// Quay về đăng nhập
        /// </summary>
        private void Backward_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login());
        }

        /// <summary>
        /// Đăng ký
        /// </summary>
        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (editUsername.Text.Length == 0 || editPassword.Password.Length == 0
                || editPassword2.Password.Length == 0 || editEmail.Text.Length == 0
                || editFirstname.Text.Length == 0 || editLastname.Text.Length == 0) {
                new Dialog(Window.GetWindow(this), "Vui lòng nhập đầy đủ thông tin").ShowDialog();
            }
            else if (!editPassword.Password.Equals(editPassword2.Password)) {
                new Dialog(Window.GetWindow(this), "Mật khẩu không khớp").ShowDialog();
            }
            else if (editPassword.Password.Length < 6) {
                new Dialog(Window.GetWindow(this), "Mật khẩu chứa ít nhất 6 ký tự").ShowDialog();
            }
            else if (!new EmailAddressAttribute().IsValid(editEmail.Text)) {
                new Dialog(Window.GetWindow(this), "Định dạng email không hợp lệ").ShowDialog();
            }
            else if (editFaculty.Tag.Equals("None")) {
                new Dialog(Window.GetWindow(this), "Vui lòng chọn khoa").ShowDialog();
            }
            else {
                Dialog confirm = new Dialog(Window.GetWindow(this), "Xác nhận đăng ký?");
                if (confirm.ShowDialog() == true)
                {
                    // Tham số cần truyền
                    RequestParams param = new RequestParams
                    {
                        ["userid"] = editUsername.Text,
                        ["password"] = editPassword.Password,
                        ["firstname"] = editFirstname.Text,
                        ["lastname"] = editLastname.Text,
                        ["email"] = editEmail.Text,
                        ["faculty"] = editFaculty.Tag.ToString()
                    };

                    // Đăng ký
                    HttpResponse loginResult = new HttpRequest()
                        .Post(MainWindow.domainURL + "/login/register", param);
                    new Dialog(Window.GetWindow(this), loginResult.ToString()).ShowDialog();

                    if (loginResult.StatusCode == xNet.HttpStatusCode.OK) NavigationService.GoBack();
                }
            }
        }

        /// <summary>
        /// Chọn một khoa
        /// </summary>
        private void EditFaculty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (editFaculty.SelectedIndex > -1)
            {
                editFaculty.Tag = faculties[editFaculty.SelectedIndex].facultyid;
            }
        }
    }
}
