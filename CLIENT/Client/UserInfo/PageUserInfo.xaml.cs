using System;
using System.Collections.Generic;
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

namespace Client.UserInfo
{
    public partial class PageUserInfo : Page
    {
        public PageUserInfo()
        {
            InitializeComponent();
            editFirstname.Text = MainWindow.user.firstname;
            editLastname.Text = MainWindow.user.lastname;
            editUsername.Text = MainWindow.user.userid;
            editPassword.Password = MainWindow.user.password;
            editPassword2.Password = MainWindow.user.password;
            editEmail.Text = MainWindow.user.email;
            editFaculty.Text = MainWindow.user.faculty;
            new Thread(() =>
            {
                var httpRequest = new HttpRequest();
                httpRequest.Cookies = MainWindow.cookies;
                string response = httpRequest.Get(MainWindow.domainURL + "/faculties/" + MainWindow.user.userid).ToString();
                Dispatcher.Invoke(() => { editFaculty.Text = response; });

            }).Start();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (btnSave.Tag.Equals("Sửa"))
            {
                editFirstname.IsEnabled = true;
                editLastname.IsEnabled = true;
                editEmail.IsEnabled = true;
                editPassword.IsEnabled = true;
                editPassword2.IsEnabled = true;
                editPassword.Focus();
                editPassword.SelectAll();
                btnSave.Content = "Lưu";
                btnSave.Tag = "Lưu";
            }
            else
            {
                if (editFirstname.Text.Length == 0
                    || editLastname.Text.Length == 0
                    || editEmail.Text.Length == 0
                    || editPassword.Password.Length == 0
                    || editPassword2.Password.Length == 0)
                {
                    new Dialog(Window.GetWindow(this), "Vui lòng nhập đầy đủ thông tin").ShowDialog();
                    return;
                }

                if (!editPassword.Password.Equals(editPassword2.Password))
                {
                    new Dialog(Window.GetWindow(this), "Mật khẩu không trùng nhau").ShowDialog();
                    return;
                }

                if (new Dialog(Window.GetWindow(this), "Xác nhận lưu thông tin?").ShowDialog() == true)
                {
                    var httpRequest = new HttpRequest();
                    httpRequest.Cookies = MainWindow.cookies;
                    httpRequest.AddParam("firstname", editFirstname.Text);
                    httpRequest.AddParam("lastname", editLastname.Text);
                    httpRequest.AddParam("email", editEmail.Text);
                    httpRequest.AddParam("password", editPassword.Password);
                    HttpResponse updateInfo = httpRequest.Raw(HttpMethod.PUT, MainWindow.domainURL + "/users/" + MainWindow.user.userid);
                    new Dialog(Window.GetWindow(this), updateInfo.ToString()).ShowDialog();
                    if (updateInfo.StatusCode == xNet.HttpStatusCode.OK)
                    {
                        editFirstname.IsEnabled = false;
                        editLastname.IsEnabled = false;
                        editEmail.IsEnabled = false;
                        editPassword.IsEnabled = false;
                        editPassword2.IsEnabled = false;
                        btnSave.Content = "Sửa";
                        btnSave.Tag = "Sửa";
                        MainWindow.user.firstname = editFirstname.Text;
                        MainWindow.user.lastname = editLastname.Text;
                        MainWindow.user.email = editEmail.Text;
                        MainWindow.user.password = editPassword.Password;
                    }
                }
            }
        }
    }
}
