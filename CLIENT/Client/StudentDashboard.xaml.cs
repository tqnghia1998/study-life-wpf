using Client.HomeWork;
using Client.Statistic;
using Client.SubjectStudent;
using Client.UserInfo;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for StudentDashboard.xaml
    /// </summary>
    public partial class StudentDashboard : Page
    {
        public StudentDashboard()
        {
            InitializeComponent();
        }

        #region Xử lý hiệu ứng
        /// <summary>
        /// Hiệu ứng khi rê chuột vào hình
        /// </summary>
        private void img_MouseMove(object sender, MouseEventArgs e)
        {
            DropShadowEffect effect = new DropShadowEffect()
            {
                Color = Colors.DarkGray,
                Direction = 270,
                BlurRadius = 20,
                ShadowDepth = 10
            };

            Image img = sender as Image;
            if (img.Tag.Equals("imgManageHomework"))
                manageHomework.Effect = effect;
            else if (img.Tag.Equals("imgManageSubject"))
                manageSubject.Effect = effect;
            else if (img.Tag.Equals("imgManageSchedule"))
                manageSchedule.Effect = effect;
            else
            {
                userInfo.Effect = effect;
            }
        }

        /// <summary>
        /// Hiệu ứng khi ra chuột ra khỏi hình
        /// </summary>
        private void img_MouseLeave(object sender, MouseEventArgs e)
        {
            DropShadowEffect effect = new DropShadowEffect()
            {
                Color = Colors.DarkGray,
                Direction = 270,
                BlurRadius = 20,
                ShadowDepth = 5
            };

            Image img = sender as Image;
            if (img.Tag.Equals("imgManageHomework"))
                manageHomework.Effect = effect;
            else if (img.Tag.Equals("imgManageSubject"))
                manageSubject.Effect = effect;
            else if (img.Tag.Equals("imgManageSchedule"))
                manageSchedule.Effect = effect;
            else
            {
                userInfo.Effect = effect;
            }
        }
        #endregion

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            if (img.Tag.Equals("imgManageHomework"))
            { 
                NavigationService.Navigate(new PageHomework());
            }
            else if (img.Tag.Equals("imgManageSubject"))
            {
                NavigationService.Navigate(new PageApplySubject());
            }
            else if (img.Tag.Equals("imgManageSchedule"))
            {
                NavigationService.Navigate(new PageStatistic());
            }
            else
            {
                NavigationService.Navigate(new PageUserInfo());
            }
        }
    }
}
