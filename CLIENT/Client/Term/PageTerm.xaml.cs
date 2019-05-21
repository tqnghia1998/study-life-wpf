using Client.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using xNet;

namespace Client.Term
{
    public partial class PageTerm : Page
    {
        // Danh sách học kỳ và các năm
        ObservableCollection<CTerm> terms;
        public ObservableCollection<string> listTermYear { get; set; }

        public PageTerm()
        {
            InitializeComponent();
            listTermYear = new ObservableCollection<string>();
            for (int i = 2000; i < 2025; i++)
            {
                editTermYear.Items.Add(i.ToString() + "-" + (i+1).ToString());
            }

            new Thread(() =>
            {
                try
                {
                    // Lấy JSON các học kỳ về
                    HttpRequest http = new HttpRequest();
                    http.Cookies = MainWindow.cookies;
                    string httpResponse = http.Get(MainWindow.domainURL + "/terms").ToString();

                    if (httpResponse.Equals("Đã hết phiên hoạt động"))
                    {
                        new Dialog(Window.GetWindow(this), httpResponse).ShowDialog();
                    }
                    else
                    {
                        // Parse thành các đối tượng CTerm
                        terms = JsonConvert.DeserializeObject<ObservableCollection<CTerm>>(httpResponse);

                        // Thêm vào Combobox
                        Dispatcher.Invoke(() =>
                        {
                            listTerms.ItemsSource = terms;
                            ProgressBar.Visibility = Visibility.Hidden;
                        });

                        // Thêm vào danh sách năm để tìm kiếm
                        foreach (CTerm term in terms)
                        {
                            listTermYear.Add(term.termyear);
                        };
                    }
                }
                catch (Exception) { }
            }).Start();

            // Thiết lập thanh tìm kiếm
            searchBar.DataContext = this;
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
        /// Tự động điều chỉnh kích thước cột
        /// </summary>
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Window win = Window.GetWindow(this);
            indexTerm.Width = win.Width - 790;
        }

        /// <summary>
        /// Click một học kỳ
        /// </summary>
        private void ListTerm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Lấy đối tượng CTerm tương ứng
            if (listTerms.SelectedItem == null) return;
            CTerm term = listTerms.SelectedItem as CTerm;

            // Đưa thông tin vào các TextBox
            editTermIndex.Text = term.termindex;
            editTermYear.Text = term.termyear;
            editBeginDate.Text = term.begindate.ToShortDateString();
            editEndDate.Text = term.enddate.ToShortDateString();

            // Bật 2 button Sửa và Xem
            btnSeeStatistic.IsEnabled = true;
            btnUpdateTerm.IsEnabled = true;
        }

        /// <summary>
        /// Thêm một học kỳ (chỉ nhập dữ liệu, chưa gửi lên server)
        /// </summary>
        private void BtnAddTerm_Click(object sender, RoutedEventArgs e)
        {
            // Kéo ListView lên đầu và vô hiệu hóa tạm thời
            if (listTerms.Items.Count > 0)
            {
                listTerms.ScrollIntoView(listTerms.Items[0]);
            }
            listTerms.SelectedIndex = -1;
            listTerms.IsEnabled = false;

            // Bật và thay nội dung 2 button Sửa và Xem
            btnUpdateTerm.IsEnabled = true;
            btnUpdateTerm.Content = "Lưu";
            btnUpdateTerm.Tag = "Thêm";
            btnSeeStatistic.IsEnabled = true;
            btnSeeStatistic.Content = "Hủy";

            // Tắt button Thêm
            btnAddTerm.IsEnabled = false;

            // Cho phép người dùng chỉnh sửa các TextBox
            editTermIndex.IsEnabled = true;
            editTermIndex.SelectedIndex = 0;
            editTermYear.IsEnabled = true;
            editTermYear.SelectedIndex = 0;
            editBeginDate.IsEnabled = true;
            editBeginDate.Text = string.Empty;
            editEndDate.IsEnabled = true;
            editEndDate.Text = string.Empty;

            // Focus vào Termindex
            editTermIndex.Focus();
        }

        /// <summary>
        /// Sửa/Gửi dữ liệu lên server
        /// </summary>
        private void BtnUpdateTerm_Click(object sender, RoutedEventArgs e)
        {
            // Nếu nội dung button là "Sửa" thì user muốn sửa một học kỳ (bắt đầu nhập thông tin)
            if (btnUpdateTerm.Content.Equals("Sửa"))
            {
                // Thay nội dung các button
                btnUpdateTerm.Content = "Lưu";
                btnUpdateTerm.Tag = "Sửa";
                btnSeeStatistic.Content = "Hủy";
                btnAddTerm.IsEnabled = false;

                // Cho phép chỉnh sửa các TextBox
                editBeginDate.IsEnabled = true;
                editEndDate.IsEnabled = true;
                editBeginDate.Focus();

                // Tạm thời vô hiệu hóa List View
                listTerms.IsEnabled = false;
            }
            // Nếu nội dung button là "Lưu" thì user muốn xác nhận việc thêm/sửa vừa làm
            else
            {
                // Kiểm tra dữ liệu có nhập đầy đủ không
                if (editTermIndex.Text.Length == 0
                    || editTermYear.Text.Length == 0
                    || editBeginDate.Text.Length == 0
                    || editEndDate.Text.Length == 0)
                {
                    var dialogError1 = new Dialog(Window.GetWindow(this), "Vui lòng nhập đầy đủ các thông tin");
                    dialogError1.ShowDialog();
                    return;
                }

                // Tạo đối tượng từ các TextBox
                CTerm term = new CTerm()
                {
                    termindex = editTermIndex.Text.Length == 0 ? null : editTermIndex.Text,
                    termyear = editTermYear.Text.Length == 0 ? null : editTermYear.Text,
                    begindate = DateTime.Parse(editBeginDate.Text),
                    enddate = DateTime.Parse(editEndDate.Text)
                };

                // So sánh ngày có hợp lệ
                if (DateTime.Compare(term.begindate, term.enddate) >= 0)
                {
                    var dialogError2 = new Dialog(Window.GetWindow(this), "Ngày bắt đầu phải trước ngày kết thúc");
                    dialogError2.ShowDialog();
                    return;
                }

                // Tham số cần truyền
                var request = new HttpRequest();
                request.Cookies = MainWindow.cookies;
                request.AddParam("termindex", term.termindex);
                request.AddParam("termyear", term.termyear);
                request.AddParam("begindate", term.begindate.ToString("yyyy-MM-dd"));
                request.AddParam("enddate", term.enddate.ToString("yyyy-MM-dd"));

                #region GỬI DỮ LIỆU LÊN SERVER
                if (btnUpdateTerm.Tag.Equals("Thêm"))
                {
                    // Hiện thông báo xác nhận
                    var dialog = new Dialog(Window.GetWindow(this), "Thêm học kỳ đã nhập?");
                    if (dialog.ShowDialog() == false) return;

                    // Gửi dữ liệu
                    HttpResponse addTermResult = request.Raw(HttpMethod.POST, MainWindow.domainURL + "/terms");

                    new Dialog(Window.GetWindow(this), addTermResult.ToString()).ShowDialog();
                    if (addTermResult.StatusCode != xNet.HttpStatusCode.OK) return;

                    // Cập nhật lên ListView
                    terms.Add(term);
                    listTerms.SelectedIndex = terms.Count - 1;
                    listTerms.ScrollIntoView(listTerms.Items[terms.Count - 1]);
                }
                else
                {
                    // Hiện thông báo xác nhận
                    var dialog = new Dialog(Window.GetWindow(this), "Xác nhận sửa học kỳ?");
                    if (dialog.ShowDialog() == false) return;

                    // Gửi dữ liệu
                    HttpResponse updateTermResult = request.Raw(HttpMethod.PUT, MainWindow.domainURL + "/terms/" + term.termindex + "/" + term.termyear);

                    new Dialog(Window.GetWindow(this), updateTermResult.ToString()).ShowDialog();
                    if (updateTermResult.StatusCode != xNet.HttpStatusCode.OK) return;

                    // Cập nhật lên List View
                    int curIndex = listTerms.SelectedIndex;
                    terms.Insert(curIndex + 1, term);
                    terms.RemoveAt(curIndex);
                    listTerms.SelectedIndex = curIndex;
                }
                #endregion

                // Reset nội dung 2 button Sửa và Xóa
                btnUpdateTerm.Content = "Sửa";
                btnSeeStatistic.Content = "Xem số liệu";

                // Vô hiệu hóa các TextBox
                editTermIndex.IsEnabled = false;
                editTermYear.IsEnabled = false;
                editBeginDate.IsEnabled = false;
                editEndDate.IsEnabled = false;

                // Bật lại button Thêm và List View
                btnAddTerm.IsEnabled = true;
                listTerms.IsEnabled = true;
            }
        }

        /// <summary>
        /// Nút Xem hoặc Hủy
        /// </summary>
        private void BtnSeeStatistic_Click(object sender, RoutedEventArgs e)
        {
            // Nếu user muốn hủy bỏ dữ liệu đang nhập
            if (btnSeeStatistic.Content.Equals("Hủy"))
            {
                // Hiện thông báo xác nhận
                var dialog = new Dialog(Window.GetWindow(this), "Hủy bỏ dữ liệu đã nhập?");
                if (dialog.ShowDialog() == false) return;

                // Hủy cho phép chỉnh sửa các TextBox
                editTermIndex.IsEnabled = false;
                editTermYear.IsEnabled = false;
                editBeginDate.IsEnabled = false;
                editEndDate.IsEnabled = false;

                // Làm sạch các TextBox nếu vừa định Thêm mới
                if (btnUpdateTerm.Tag.Equals("Thêm"))
                {
                    editTermIndex.Text = string.Empty;
                    editTermYear.Text = string.Empty;
                    editBeginDate.Text = string.Empty;
                    editEndDate.Text = string.Empty;

                    // Tắt luôn hai button Sửa và Xem (nếu vừa định Sửa thì thôi)
                    btnUpdateTerm.IsEnabled = false;
                    btnSeeStatistic.IsEnabled = false;
                }
                // Reset dữ liệu cũ nếu vừa định Sửa
                else
                {
                    // Lấy đối tượng CTerm ương ứng
                    CTerm term = listTerms.SelectedItem as CTerm;
                    editTermIndex.Text = term.termindex;
                    editTermYear.Text = term.termyear;
                    editBeginDate.Text = term.begindate.ToShortDateString();
                    editEndDate.Text = term.enddate.ToShortDateString();
                }

                // Reset nội dung 2 button Sửa và Xóa
                btnUpdateTerm.Content = "Sửa";
                btnSeeStatistic.Content = "Xem số liệu";
                btnAddTerm.IsEnabled = true;

                // Bật lại List View
                listTerms.IsEnabled = true;
            }
            // Nếu muốn xem thống kê
            else
            {

            }
        }

        /// <summary>
        /// Tìm kiếm theo năm
        /// </summary>
        private void SearchBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (searchBar.Text == string.Empty)
                {
                    if (listTerms.Items.Count > 0)
                    {
                        listTerms.SelectedIndex = 0;
                        listTerms.ScrollIntoView(listTerms.Items[0]);
                    }
                    return;
                }
                editTermIndex.Text = string.Empty;
                editTermYear.Text = string.Empty;
                editBeginDate.Text = string.Empty;
                editEndDate.Text = string.Empty;
                foreach (CTerm term in terms)
                {
                    if (term.termyear.ToUpper().Contains(searchBar.Text.ToUpper()))
                    {
                        listTerms.SelectedItem = term;
                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Hàm chuyển đổi STT cho List View
    /// </summary>
    public class DataConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
        {
            ListViewItem item = (ListViewItem)value;
            ListView listView = ItemsControl.ItemsControlFromItemContainer(item) as ListView;
            int index = listView.ItemContainerGenerator.IndexFromContainer(item) + 1;
            return index.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
