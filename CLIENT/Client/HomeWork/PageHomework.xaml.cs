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

namespace Client.HomeWork
{
    /// <summary>
    /// Interaction logic for PageHomework.xaml
    /// </summary>
    public partial class PageHomework : Page
    {
        List<Demo> demos;

        public PageHomework()
        {
            InitializeComponent();

           

            demos = new List<Demo>();
            demos.Add(new Demo("BT01", "Tên bài tập: Bài tập seidel", "Môn: Phương pháp tính" , 60, "Hạn chót: 20/10/2019", "Mô tả: Bài tập bắt buộc"));
            demos.Add(new Demo("BT01", "Tên bài tập: Bài tập phương pháp lặp", "Môn: Phương pháp tính", 80, "Hạn chót: 08/10/2019", "Mô tả: Bài tập bắt buộc, làm trên giấy"));
            demos.Add(new Demo("BT01", "Tên bài tập: Bài tập seidel", "Môn: Phương pháp tính", 60, "Hạn chót: 20/10/2019", "Mô tả: Bài tập bắt buộc"));
            demos.Add(new Demo("BT01", "Tên bài tập: Bài tập seidel", "Môn: Phương pháp tính", 60, "Hạn chót: 20/10/2019", "Mô tả: Bài tập bắt buộc"));
            demos.Add(new Demo("BT01", "Tên bài tập: Bài tập seidel", "Môn: Phương pháp tính", 60, "Hạn chót: 20/10/2019", "Mô tả: Bài tập bắt buộc"));
            demos.Add(new Demo("BT01", "Tên bài tập: Bài tập seidel", "Môn: Phương pháp tính", 60, "Hạn chót: 20/10/2019", "Mô tả: Bài tập bắt buộc"));

            //this.DataContext = new Demo("Ngọc","TBN");
            spListHomeWork.ItemsSource = demos;
        }

        public class Demo
        {
            public string homeworkid { get; set; }
            public string homeworkname { get; set; }
            public string subjectname { get; set; }
            public int process { get; set; }
            public string deadline { get; set; }
            public string description { get; set; }

            public Demo(string homeworkid, string homeworkname, string subjectname, int process, string deadline, string description)
            {
                this.homeworkid = homeworkid;
                this.homeworkname = homeworkname;
                this.subjectname = subjectname;
                this.process = process;
                this.deadline = deadline;
                this.description = description;
            }

            public Demo()
            {
            }
        }

        //Demo demo;
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //var item = sender as ListViewItem;
            //demo = new Demo();
            //demo = item.DataContext as Demo;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
