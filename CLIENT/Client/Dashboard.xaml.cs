using Client.Faculty;
using Client.Statistic;
using Client.Subject;
using Client.Term;
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
    public partial class Dashboard : Page
    {
        public Dashboard()
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
            if (img.Tag.Equals("imgManageFaculty"))
                manageFaculty.Effect = effect;
            else if (img.Tag.Equals("imgManageSubject"))
                manageSubject.Effect = effect;
            else if (img.Tag.Equals("imgManageTerm"))
                manageTerm.Effect = effect;
            else manageStatistic.Effect = effect;
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
            if (img.Tag.Equals("imgManageFaculty"))
                manageFaculty.Effect = effect;
            else if (img.Tag.Equals("imgManageSubject"))
                manageSubject.Effect = effect;
            else if (img.Tag.Equals("imgManageTerm"))
                manageTerm.Effect = effect;
            else manageStatistic.Effect = effect;
        }
        #endregion

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            if (img.Tag.Equals("imgManageFaculty"))
            {
                NavigationService.Navigate(new PageFaculty());
            }
            else if (img.Tag.Equals("imgManageSubject"))
            {
                NavigationService.Navigate(new PageSubject());
            }
            else if (img.Tag.Equals("imgManageTerm"))
            {
                NavigationService.Navigate(new PageTerm());
            }
            else
            {
                NavigationService.Navigate(new PageStatistic());
            }
        }
    }
}