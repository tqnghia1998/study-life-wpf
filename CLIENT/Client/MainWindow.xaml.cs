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
using System.Windows.Navigation;
using System.Windows.Shapes;
using xNet;

namespace Client
{
    public partial class MainWindow : Window
    {
        public static string domainURL;
        public static CookieDictionary cookies;

        public MainWindow()
        {
            InitializeComponent();
            _mainFrame.Navigate(new Login());
            domainURL = "http://localhost:6969";
        }

        /// <summary>
        /// Minimize, Restore, Close
        /// </summary>
        private void BtnCommands_Click(object sender, RoutedEventArgs e)
        {
            Button curButton = sender as Button;
            if (curButton.Tag.Equals("btnClose")) Close();
            else if (curButton.Tag.Equals("btnMinim")) WindowState = WindowState.Minimized;
            else if (curButton.Tag.Equals("btnMaxim")) WindowState =
                    WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        /// <summary>
        /// Di chuyển cửa sổ
        /// </summary>
        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) DragMove();
        }
    }
}
