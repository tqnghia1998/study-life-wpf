using Client.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using xNet;
using Button = System.Windows.Controls.Button;

namespace Client
{
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Button Đăng nhập
        /// </summary>
        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            // Tham số username và password
            RequestParams param = new RequestParams
            {
                ["username"] = editUsername.Text,
                ["password"] = editPassword.Password
            };

            // Đăng nhập
            try
            {
                HttpRequest http = new HttpRequest();
                http.Cookies = new CookieDictionary();
                HttpResponse loginResult = http.Post(MainWindow.domainURL + "/login", param);

                if (loginResult.StatusCode != HttpStatusCode.OK)
                {
                    new Dialog(Window.GetWindow(this), loginResult.ToString()).ShowDialog();
                }
                else
                {
                    new Dialog(Window.GetWindow(this), "Đăng nhập thành công").ShowDialog();
                    MainWindow.user = JsonConvert.DeserializeObject<CUser>(loginResult.ToString());
                    MainWindow.cookies = http.Cookies;
                    Clipboard.SetText(http.Cookies.ToString());
                    NavigationService.Navigate(new Dashboard());
                }
            }
            catch (Exception) {}
        }

        /// <summary>
        /// Nhấn Enter để đăng nhập
        /// </summary>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) btnSignIn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        /// <summary>
        /// Đăng ký tài khoản
        /// </summary>
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Register());
        }
    }
}
