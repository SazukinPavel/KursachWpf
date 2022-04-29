using Kursach.Models;
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

namespace Kursach.Controls
{
    /// <summary>
    /// Логика взаимодействия для CourseCard.xaml
    /// </summary>
    public partial class CourseCard : UserControl
    {

        public delegate void onOpen(Course course);
        event onOpen onOpenEvent;
        public Course Course { get; set; }
        public CourseCard(Course course,onOpen onOpen)
        {
            InitializeComponent();
            Course = course;
            RenderCourse();
            onOpenEvent += onOpen;
        }

        void RenderCourse()
        {
            name.Text = Course.name;
            if (Course.authors != null)
            {
                author.Text = "Автор:" + Course.authors[0].name;
            }
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            onOpenEvent(Course);
        }
    }
}
