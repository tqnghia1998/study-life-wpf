using Client.Classes;
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

namespace Client.SubjectStudent
{
    /// <summary>
    /// Interaction logic for PageApplySubject.xaml
    /// </summary>
    public partial class PageApplySubject : Page
    {
        List<CSubject> cSubjects;
        public PageApplySubject()
        {
            InitializeComponent();
            cSubjects = new List<CSubject>();
            cSubjects.Add(new CSubject("Mã môn học: PPT0002", "Phương pháp tính", 4, "Giảng viên: Vũ Đỗ Huy Cường", "Học kỳ: 2", "Năm: 3", "Khoa: CNTT"));
            cSubjects.Add(new CSubject("Mã môn học: HDH0391", "Hệ điều hành", 4, "Giảng viên: Trần Trung Dũng", "Học kỳ: 1", "Năm: 3", "Khoa: CNTT"));

            cSubjects.Add(new CSubject("Mã môn học: PPT0002", "Phương pháp tính", 4, "Giảng viên: Vũ Đỗ Huy Cường", "Học kỳ: 2", "Năm: 3", "Khoa: CNTT"));
            cSubjects.Add(new CSubject("Mã môn học: HDH0391", "Hệ điều hành", 4, "Giảng viên: Trần Trung Dũng", "Học kỳ: 1", "Năm: 3", "Khoa: CNTT"));

            cSubjects.Add(new CSubject("Mã môn học: PPT0002", "Phương pháp tính", 4, "Giảng viên: Vũ Đỗ Huy Cường", "Học kỳ: 2", "Năm: 3", "Khoa: CNTT"));
            cSubjects.Add(new CSubject("Mã môn học: HDH0391", "Hệ điều hành", 4, "Giảng viên: Trần Trung Dũng", "Học kỳ: 1", "Năm: 3", "Khoa: CNTT"));
            spListSubject.ItemsSource = cSubjects;

        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
