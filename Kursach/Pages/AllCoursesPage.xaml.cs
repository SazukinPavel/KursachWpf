using Kursach.Controls;
using Kursach.Models;
using Kursach.Services;
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

namespace Kursach.Pages
{
    /// <summary>
    /// Логика взаимодействия для AllCoursesPage.xaml
    /// </summary>
    public partial class AllCoursesPage : Page
    {

        List<Course> Courses;
        CoursesService CoursesService;

        public AllCoursesPage()
        {
            ShowsNavigationUI = false;
            InitializeComponent();
            Courses = new List<Course>();
            CoursesService = new CoursesService();
        }

        async System.Threading.Tasks.Task FetchCourses()
        {
            Courses = (await CoursesService.GetCourses()).Data.ToList();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (Courses == null)
            {
                RenderCoursePanel();
            }
        }

        async void RenderCoursePanel()
        {
            await FetchCourses();
            foreach (var course in Courses)
            {
                courses.Children.Add(new CourseCard(course, GoToCoursePage));
            }
        }

        public void GoToCoursePage(Course course)
        {
            this.NavigationService.Navigate(new CoursePage(course,this));
        }

        private void GoBackClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new UserPage());
        }
    }
}
