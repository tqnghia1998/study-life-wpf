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

namespace Client.SubjectStudent
{
    /// <summary>
    /// Interaction logic for PageApplySubject.xaml
    /// </summary>
    public partial class PageApplySubject : Page
    {
        public PageApplySubject()
        {
            InitializeComponent();

            new Thread(() =>
            {
                try
                {
                    // Lấy JSON các khoa về
                    HttpRequest http = new HttpRequest();
                    http.Cookies = MainWindow.cookies;
                    string httpResponseSubject = http.Get(MainWindow.domainURL + "/register").ToString();
                    if (httpResponseSubject.Equals("Đã hết phiên hoạt động"))
                    {
                        new Dialog(Window.GetWindow(this), httpResponseSubject).ShowDialog();
                    }
                    else
                    {
                        ObservableCollection<CTerm> termyears;
                        // Parse thành các đối tượng task
                        termyears = JsonConvert.DeserializeObject<ObservableCollection<CTerm>>(httpResponseSubject);

                        // Thêm vào Combobox
                        Dispatcher.Invoke(() =>
                        {
                            termyear.ItemsSource = termyears;
                        });
                    }
                }
                catch (Exception) { }
            }).Start();
        }

        private void Termyear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ObservableCollection<CTerm> termindexs;
                string currenttermyear = (termyear.SelectedItem as CTerm).termyear;
                // Lấy JSON các khoa về
                HttpRequest http = new HttpRequest();
                http.Cookies = MainWindow.cookies;
                string httpResponseSubject = http.Get(MainWindow.domainURL + "/register/" + currenttermyear).ToString();
                if (httpResponseSubject.Equals("Đã hết phiên hoạt động"))
                {
                    new Dialog(Window.GetWindow(this), httpResponseSubject).ShowDialog();
                }
                else
                {
                    // Parse thành các đối tượng task
                    termindexs = JsonConvert.DeserializeObject<ObservableCollection<CTerm>>(httpResponseSubject);

                    // Thêm vào Combobox
                    Dispatcher.Invoke(() =>
                    {
                        termindex.ItemsSource = termindexs;
                        termindex.SelectedIndex = 0;
                    });

                }
            }
            catch (Exception) { }
        }

        private void Termindex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string currenttermyear = (termyear.SelectedItem as CTerm).termyear;
                string currenttermindex = (termindex.SelectedItem as CTerm).termindex;
                // Lấy JSON các khoa về
                HttpRequest http = new HttpRequest();
                http.Cookies = MainWindow.cookies;
                string httpResponseSubject = http.Get(MainWindow.domainURL + "/register/" + currenttermyear + "/" + currenttermindex).ToString();
                if (httpResponseSubject.Equals("Đã hết phiên hoạt động"))
                {
                    new Dialog(Window.GetWindow(this), httpResponseSubject).ShowDialog();
                }
                else
                {
                    ObservableCollection<CRegister> subjects;
                    // Parse thành các đối tượng task
                    subjects = JsonConvert.DeserializeObject<ObservableCollection<CRegister>>(httpResponseSubject);

                    // Thêm vào Combobox
                    spListSubject.ItemsSource = subjects;
                }
            }
            catch (Exception) { }
        }

        CRegister current;
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            current = new CRegister();
            var item = sender as ListViewItem;
            if (item != null)
            {
                current = (CRegister)item.DataContext;
            }
        }

        private void Registsubject_Click(object sender, RoutedEventArgs e)
        {
            // Tham số cần truyền
            var request = new HttpRequest();
            request.Cookies = MainWindow.cookies;
            request.AddParam("subjectid", current.subjectid);
            // Gửi dữ liệu lên server
            HttpResponse addTermResult = request.Raw(HttpMethod.POST, MainWindow.domainURL + "/register");

            new Dialog(Window.GetWindow(this), addTermResult.ToString()).ShowDialog();
            if (addTermResult.StatusCode != xNet.HttpStatusCode.OK) return;
        }

        ObservableCollection<CRegister> getRegisted()
        {
            ObservableCollection<CRegister> registed;
            HttpRequest http = new HttpRequest();
            http.Cookies = MainWindow.cookies;
            string httpResponseSubject = http.Get(MainWindow.domainURL + "/registed").ToString();
            if (httpResponseSubject.Equals("Đã hết phiên hoạt động"))
            {
                new Dialog(Window.GetWindow(this), httpResponseSubject).ShowDialog();
                return null;
            }
            else
            {
                // Parse thành các đối tượng task
                registed = JsonConvert.DeserializeObject<ObservableCollection<CRegister>>(httpResponseSubject);
                return registed;
            }
        }

        ObservableCollection<CRegister> getUnRegisted()
        {
            ObservableCollection<CRegister> unregisted;
            HttpRequest http = new HttpRequest();
            http.Cookies = MainWindow.cookies;
            string httpResponseSubject = http.Get(MainWindow.domainURL + "/unregisted").ToString();
            if (httpResponseSubject.Equals("Đã hết phiên hoạt động"))
            {
                new Dialog(Window.GetWindow(this), httpResponseSubject).ShowDialog();
                return null;
            }
            else
            {
                // Parse thành các đối tượng task
                unregisted = JsonConvert.DeserializeObject<ObservableCollection<CRegister>>(httpResponseSubject);
                return unregisted;
            }
        }

        private void TypeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (typeList.SelectedIndex == 0)
            {
                spListRegisted.Visibility = Visibility.Visible;
                spListSubject.Visibility = Visibility.Hidden;
                Termindex_SelectionChanged(null, null);
            }
            else
            {
                spListRegisted.Visibility = Visibility.Hidden;
                spListSubject.Visibility = Visibility.Visible;
                spListRegisted.ItemsSource = getRegisted();

            }
        }

        #region Hiệu ứng combox
        private void Combobox_DropDownOpened(object sender, EventArgs e)
        {
            (sender as ComboBox).Background = Brushes.LightGray;
        }

        private void Combobox_DropDownClosed(object sender, EventArgs e)
        {
            (sender as ComboBox).Background = Brushes.Transparent;
        }
        #endregion
    }
}
