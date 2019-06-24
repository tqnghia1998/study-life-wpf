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
        ObservableCollection<CRegister> unregisted;
        ObservableCollection<CRegister> registed;
        public PageApplySubject()
        {
            InitializeComponent();
            new Thread(() =>
            {
                try
                {
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
                            this.DataContext = this;
                        });
                    }
                }
                catch (Exception) { }
            }).Start();
        }

        #region Combobox
        string currenttermyear;
        string currenttermindex;
        private void Termyear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ObservableCollection<CTerm> termindexs;
                currenttermyear = (termyear.SelectedItem as CTerm).termyear;
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
                    currenttermyear = (termyear.SelectedItem as CTerm).termyear;

                    // Thêm vào Combobox
                    Dispatcher.Invoke(() =>
                    {
                        termindex.ItemsSource = termindexs;
                        termindex.SelectedIndex = 0;
                        searchKey.Text = "";
                    });

                }
            }
            catch (Exception) { }
        }

        private void Termindex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                currenttermindex = (termindex.SelectedItem as CTerm).termindex;
                spListSubject.ItemsSource = getUnRegisted();
                typeList.SelectedIndex = 0;
                searchKey.Text = "";
                totalCredit();
            }
            catch (Exception) { }
        }

        private void TypeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (typeList.SelectedIndex == 0)
            {
                spListRegisted.Visibility = Visibility.Hidden;
                spListSubject.Visibility = Visibility.Visible;
                Termindex_SelectionChanged(null, null);

            }
            else
            {
                spListRegisted.Visibility = Visibility.Visible;
                spListSubject.Visibility = Visibility.Hidden;
                spListRegisted.ItemsSource = getRegisted();
            }
            searchKey.Text = "";
            totalCredit();
        }

        CRegister current;
        /// <summary>
        /// Nhận dòng khi nhấn nút đăng ký hoặc huỷ đăng ký
        /// </summary>
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            current = new CRegister();
            var item = sender as ListViewItem;
            if (item != null)
            {
                current = (CRegister)item.DataContext;
            }
        }
        #endregion
        
        /// <summary>
        /// Gửi request đăng kí
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Registsubject_Click(object sender, RoutedEventArgs e)
        {
            // Tham số cần truyền
            var request = new HttpRequest();
            request.Cookies = MainWindow.cookies;
            request.AddParam("subjectid", current.subjectid);
            // Gửi dữ liệu lên server
            HttpResponse addRegisters = request.Raw(HttpMethod.POST, MainWindow.domainURL + "/register/" + currenttermyear + "/" + currenttermindex);

            new Dialog(Window.GetWindow(this), addRegisters.ToString()).ShowDialog();
            if (addRegisters.StatusCode != xNet.HttpStatusCode.OK) return;
            TypeList_SelectionChanged(null, null);
        }

        /// <summary>
        /// Lấy danh sách môn đã đăng ký
        /// </summary>
        /// <returns>Danh sách CRegister</returns>
        ObservableCollection<CRegister> getRegisted()
        {
            HttpRequest http = new HttpRequest();
            http.Cookies = MainWindow.cookies;
            string httpResponseSubject = http.Get(MainWindow.domainURL + "/registed/" + currenttermyear + "/" + currenttermindex).ToString();
            if (httpResponseSubject.Equals("Đã hết phiên hoạt động"))
            {
                new Dialog(Window.GetWindow(this), httpResponseSubject).ShowDialog();
                return null;
            }
            else
            {
                // Parse thành các đối tượng subject
                registed = JsonConvert.DeserializeObject<ObservableCollection<CRegister>>(httpResponseSubject);
                foreach (var item in registed)
                {
                    string httpResponseSchedule = http.Get(MainWindow.domainURL + "/schedules/" + item.subjectid).ToString();
                    if (httpResponseSchedule.Equals("Đã hết phiên hoạt động"))
                    {
                        new Dialog(Window.GetWindow(this), httpResponseSchedule).ShowDialog();
                        return null;
                    }
                    else
                    {
                        ObservableCollection<CSchedule> schedules = JsonConvert.DeserializeObject<ObservableCollection<CSchedule>>(httpResponseSchedule);
                        foreach (var item2 in schedules)
                        {
                            item.day += (item2.day + ": " + item2.starttime + " - " + item2.finishtime + ", Phòng: " + item2.room + "\n");
                        }
                    }

                }
                // MessageBox.Show(registed.Count.ToString());
                return registed;
            }

        }

        /// <summary>
        /// Lấy danh sách môn chưa đăng ký
        /// </summary>
        /// <returns></returns>
        ObservableCollection<CRegister> getUnRegisted()
        {
            
            HttpRequest http = new HttpRequest();
            http.Cookies = MainWindow.cookies;
            string httpResponseSubject = http.Get(MainWindow.domainURL + "/registers/notregistered/" + currenttermyear + "/" + currenttermindex).ToString();
            if (httpResponseSubject.Equals("Đã hết phiên hoạt động"))
            {
                new Dialog(Window.GetWindow(this), httpResponseSubject).ShowDialog();
                return null;
            }
            else
            {
                // Parse thành các đối tượng task
                unregisted = JsonConvert.DeserializeObject<ObservableCollection<CRegister>>(httpResponseSubject);
                foreach (var item in unregisted)
                {
                    string httpResponseSchedule = http.Get(MainWindow.domainURL + "/schedules/" + item.subjectid).ToString();
                    if (httpResponseSchedule.Equals("Đã hết phiên hoạt động"))
                    {
                        new Dialog(Window.GetWindow(this), httpResponseSchedule).ShowDialog();
                        return null;
                    }
                    else
                    {
                        ObservableCollection<CSchedule> schedules = JsonConvert.DeserializeObject<ObservableCollection<CSchedule>>(httpResponseSchedule);
                        foreach (var item2 in schedules)
                        {
                            item.day += (item2.day + ": " + item2.starttime + " - " + item2.finishtime + ", Phòng: " + item2.room + "\n");
                        }
                    }
                }
                
                return unregisted;
            }
        }

        void totalCredit()
        {
            var sum = getRegisted().Sum(s => int.Parse(s.credit));
            totalcredit.Content = "Số tín chỉ: " + sum + "";
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

        private void Deregistsubject_Click(object sender, RoutedEventArgs e)
        {
            // Tham số cần truyền
            var request = new HttpRequest();
            request.Cookies = MainWindow.cookies;
            request.AddParam("subjectid", current.subjectid);
            // Gửi dữ liệu lên server
            HttpResponse addTermResult = request.Raw(HttpMethod.DELETE, MainWindow.domainURL + "/register");

            new Dialog(Window.GetWindow(this), addTermResult.ToString()).ShowDialog();
            if (addTermResult.StatusCode != xNet.HttpStatusCode.OK) return;

            TypeList_SelectionChanged(null, null);
        }

        private void SearchKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<CRegister> registedSearch = getRegisted();
            ObservableCollection<CRegister> unregistedSearch = getUnRegisted();

            for (int i = 0; i < registedSearch.Count; i++)
            {
                if (!((registedSearch[i].subjectid.ToLower().Contains(searchKey.Text.ToLower().Trim())
                    || registedSearch[i].subjectname.ToLower().Contains(searchKey.Text.ToLower().Trim()))))
                {
                    registedSearch.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < unregistedSearch.Count; i++)
            {
                if (!((unregistedSearch[i].subjectid.ToLower().Contains(searchKey.Text.ToLower().Trim())
                    || unregistedSearch[i].subjectname.ToLower().Contains(searchKey.Text.ToLower().Trim()))))
                {
                    unregistedSearch.RemoveAt(i);
                    i--;
                }
            }

            // Thêm vào Combobox
            Dispatcher.Invoke(() =>
            {
                spListRegisted.ItemsSource = registedSearch;
                spListSubject.ItemsSource = unregistedSearch;
            });
        }

    }
}
