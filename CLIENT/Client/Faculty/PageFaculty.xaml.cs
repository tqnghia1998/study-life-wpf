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
using xNet;

namespace Client.Faculty
{
    public partial class PageFaculty : Page
    {
        // Danh sách khoa và danh sách tên khoa
        ObservableCollection<CFaculty> faculties;
        public ObservableCollection<string> listFacultyName { get; set; }

        public PageFaculty()
        {
            InitializeComponent();
            listFacultyName = new ObservableCollection<string>();

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

                        // Thêm vào Combobox
                        Dispatcher.Invoke(() => {
                            listFaculty.ItemsSource = faculties;
                            ProgressBar.Visibility = Visibility.Hidden;
                        });

                        // Thêm vào danh sách tên khoa
                        foreach (CFaculty faculty in faculties)
                        {
                            listFacultyName.Add(faculty.facultyname);
                        };
                    }
                }
                catch (Exception) { }
            }).Start();

            // Thiết lập thanh tìm kiếm
            searchBar.DataContext = this;
        }

        /// <summary>
        /// Tự động điều chỉnh kích thước cột
        /// </summary>
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Window win = Window.GetWindow(this);
            nameFaculty.Width = win.Width - 765;
        }

        /// <summary>
        /// Click một khoa
        /// </summary>
        private void ListFaculty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Lấy đối tượng CFaculty tương ứng
            if (listFaculty.SelectedItem == null) return;
            CFaculty faculty = listFaculty.SelectedItem as CFaculty;

            // Đưa thông tin vào các TextBox
            editFacultyName.Text = faculty.facultyname;
            editFacultyId.Text = faculty.facultyid;
            editFacultyRoom.Text = faculty.facultyroom;
            editFacultyEmail.Text = faculty.facultyemail;
            editFacultyPhone.Text = faculty.facultyphone;

            // Bật 2 button Sửa và Xem
            btnSeeSubjects.IsEnabled = true;
            btnUpdateFaculty.IsEnabled = true;
            
        }

        /// <summary>
        /// Thêm một khoa (chỉ nhập dữ liệu, chưa gửi lên server)
        /// </summary>
        private void BtnAddFaculty_Click(object sender, RoutedEventArgs e)
        {
            // Kéo ListView lên đầu và vô hiệu hóa tạm thời
            if (listFaculty.Items.Count > 0)
            {
                listFaculty.ScrollIntoView(listFaculty.Items[0]);
            }
            listFaculty.SelectedIndex = -1;
            listFaculty.IsEnabled = false;

            // Bật và thay nội dung 2 button Sửa và Xem
            btnUpdateFaculty.IsEnabled = true;
            btnUpdateFaculty.Content = "Lưu";
            btnUpdateFaculty.Tag = "Thêm";
            btnSeeSubjects.IsEnabled = true;
            btnSeeSubjects.Content = "Hủy";

            // Tắt button Thêm
            btnAddFaculty.IsEnabled = false;
            
            // Cho phép người dùng chỉnh sửa các TextBox
            editFacultyId.IsEnabled = true;
            editFacultyId.Clear();
            editFacultyName.IsEnabled = true;
            editFacultyName.Clear();
            editFacultyRoom.IsEnabled = true;
            editFacultyRoom.Clear();
            editFacultyEmail.IsEnabled = true;
            editFacultyEmail.Clear();
            editFacultyPhone.IsEnabled = true;
            editFacultyPhone.Clear();

            // Focus vào Tên loại sản phẩm cho người dùng nhập
            editFacultyId.Focus();
        }

        /// <summary>
        /// Sửa/Gửi dữ liệu lên server
        /// </summary>
        private void BtnUpdateFaculty_Click(object sender, RoutedEventArgs e)
        {
            // Nếu nội dung button là "Sửa" thì user muốn sửa một khoa (bắt đầu nhập thông tin)
            if (btnUpdateFaculty.Content.Equals("Sửa"))
            {
                // Thay nội dung các button
                btnUpdateFaculty.Content = "Lưu";
                btnUpdateFaculty.Tag = "Sửa";
                btnSeeSubjects.Content = "Hủy";
                btnAddFaculty.IsEnabled = false;

                // Cho phép chỉnh sửa các TextBox
                editFacultyName.IsEnabled = true;
                editFacultyRoom.IsEnabled = true;
                editFacultyEmail.IsEnabled = true;
                editFacultyPhone.IsEnabled = true;
                editFacultyName.Focus();

                // Tạm thời vô hiệu hóa List View
                listFaculty.IsEnabled = false;
            }
            // Nếu nội dung button là "Lưu" thì user muốn xác nhận việc thêm/sửa vừa làm
            else
            {
                // Kiểm tra dữ liệu có nhập đầy đủ không
                if (editFacultyName.Text.Length == 0
                    || editFacultyId.Text.Length == 0
                    || editFacultyRoom.Text.Length == 0
                    || editFacultyEmail.Text.Length == 0
                    || editFacultyPhone.Text.Length == 0)
                {
                    var dialogError1 = new Dialog(Window.GetWindow(this), "Vui lòng nhập đầy đủ các thông tin");
                    dialogError1.ShowDialog();
                    return;
                }

                // Tạo đối tượng từ các TextBox
                CFaculty faculty = new CFaculty()
                {
                    facultyname = editFacultyName.Text.Length == 0 ? null : editFacultyName.Text,
                    facultyid = editFacultyId.Text.Length == 0 ? null : editFacultyId.Text,
                    facultyroom = editFacultyRoom.Text.Length == 0 ? null : editFacultyRoom.Text,
                    facultyemail = editFacultyEmail.Text.Length == 0 ? null : editFacultyEmail.Text,
                    facultyphone = editFacultyPhone.Text.Length == 0 ? null : editFacultyPhone.Text
                };

                // Tham số cần truyền
                var request = new HttpRequest();
                request.Cookies = MainWindow.cookies;
                request.AddParam("facultyid", faculty.facultyid);
                request.AddParam("facultyname", faculty.facultyname);
                request.AddParam("facultyroom", faculty.facultyroom);
                request.AddParam("facultyemail", faculty.facultyemail);
                request.AddParam("facultyphone", faculty.facultyphone);

                #region GỬI DỮ LIỆU LÊN SERVER
                if (btnUpdateFaculty.Tag.Equals("Thêm"))
                {
                    // Hiện thông báo xác nhận
                    var dialog = new Dialog(Window.GetWindow(this), "Thêm khoa đã nhập?");
                    if (dialog.ShowDialog() == false) return;
                    
                    // Gửi dữ liệu
                    HttpResponse addFacultyResult = request.Raw(HttpMethod.POST, MainWindow.domainURL + "/faculties");

                    new Dialog(Window.GetWindow(this), addFacultyResult.ToString()).ShowDialog();
                    if (addFacultyResult.StatusCode != xNet.HttpStatusCode.OK) return;

                    // Cập nhật lên ListView
                    faculties.Add(faculty);
                    listFaculty.SelectedIndex = faculties.Count - 1;
                    listFaculty.ScrollIntoView(listFaculty.Items[faculties.Count - 1]);
                }
                else
                {
                    // Hiện thông báo xác nhận
                    var dialog = new Dialog(Window.GetWindow(this), "Xác nhận sửa khoa?");
                    if (dialog.ShowDialog() == false) return;

                    // Gửi dữ liệu
                    HttpResponse updateFacultyResult = request.Raw(HttpMethod.PUT, MainWindow.domainURL + "/faculties/" + faculty.facultyid);

                    new Dialog(Window.GetWindow(this), updateFacultyResult.ToString()).ShowDialog();
                    if (updateFacultyResult.StatusCode != xNet.HttpStatusCode.OK) return;

                    // Cập nhật lên List View
                    int curIndex = listFaculty.SelectedIndex;
                    faculties.Insert(curIndex + 1, faculty);
                    faculties.RemoveAt(curIndex);
                    listFaculty.SelectedIndex = curIndex;
                }
                #endregion

                // Reset nội dung 2 button Sửa và Xóa
                btnUpdateFaculty.Content = "Sửa";
                btnSeeSubjects.Content = "Xem môn học";

                // Vô hiệu hóa các TextBox
                editFacultyId.IsEnabled = false;
                editFacultyName.IsEnabled = false;
                editFacultyRoom.IsEnabled = false;
                editFacultyEmail.IsEnabled = false;
                editFacultyPhone.IsEnabled = false;

                // Bật lại button Thêm và List View
                btnAddFaculty.IsEnabled = true;
                listFaculty.IsEnabled = true;
            }
        }

        /// <summary>
        /// Nút Xem hoặc Hủy
        /// </summary>
        private void BtnSeeSubjects_Click(object sender, RoutedEventArgs e)
        {
            // Nếu user muốn hủy bỏ dữ liệu đang nhập
            if (btnSeeSubjects.Content.Equals("Hủy"))
            {
                // Hiện thông báo xác nhận
                var dialog = new Dialog(Window.GetWindow(this), "Hủy bỏ dữ liệu đã nhập?");
                if (dialog.ShowDialog() == false) return;

                // Hủy cho phép chỉnh sửa các TextBox
                editFacultyName.IsEnabled = false;
                editFacultyId.IsEnabled = false;
                editFacultyRoom.IsEnabled = false;
                editFacultyPhone.IsEnabled = false;
                editFacultyEmail.IsEnabled = false;

                // Làm sạch các TextBox nếu vừa định Thêm mới
                if (btnUpdateFaculty.Tag.Equals("Thêm"))
                {
                    editFacultyName.Clear();
                    editFacultyId.Clear();
                    editFacultyRoom.Clear();
                    editFacultyPhone.Clear();
                    editFacultyEmail.Clear();

                    // Tắt luôn hai button Sửa và Xem (nếu vừa định Sửa thì thôi)
                    btnUpdateFaculty.IsEnabled = false;
                    btnSeeSubjects.IsEnabled = false;
                }
                // Reset dữ liệu cũ nếu vừa định Sửa
                else
                {
                    // Lấy đối tượng CFaculty tương ứng
                    CFaculty faculty = listFaculty.SelectedItem as CFaculty;
                    editFacultyName.Text = faculty.facultyname;
                    editFacultyId.Text = faculty.facultyid;
                    editFacultyRoom.Text = faculty.facultyroom;
                    editFacultyPhone.Text = faculty.facultyphone;
                    editFacultyEmail.Text = faculty.facultyemail;
                }

                // Reset nội dung 2 button Sửa và Xóa
                btnUpdateFaculty.Content = "Sửa";
                btnSeeSubjects.Content = "Xem sản phẩm";
                btnAddFaculty.IsEnabled = true;

                // Bật lại List View
                listFaculty.IsEnabled = true;
            }
            // Nếu muốn xem danh sách môn học
            else
            {

            }
        }

        /// <summary>
        /// Tìm kiếm khoa
        /// </summary>
        private void SearchBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (searchBar.Text == string.Empty)
                {
                    if (listFaculty.Items.Count > 0)
                    {
                        listFaculty.SelectedIndex = 0;
                        listFaculty.ScrollIntoView(listFaculty.Items[0]);
                    }
                    return;
                }
                editFacultyName.Clear();
                editFacultyId.Clear();
                editFacultyRoom.Clear();
                editFacultyPhone.Clear();
                editFacultyEmail.Clear();
                foreach (CFaculty faculty in faculties)
                {
                    if (faculty.facultyname.ToUpper().Contains(searchBar.Text.ToUpper()))
                    {
                        listFaculty.SelectedItem = faculty;
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