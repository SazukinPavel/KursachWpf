using Kursach.Controls;
using Kursach.Models;
using Kursach.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Kursach.Pages
{
    /// <summary>
    /// Логика взаимодействия для AllCoursesPage.xaml
    /// </summary>
    public partial class AllCoursesPage : Page
    {

        List<Course> Courses;
        CourseService CoursesService;

        public AllCoursesPage()
        {
            ShowsNavigationUI = false;
            InitializeComponent();
            CoursesService = new CourseService();
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
            this.NavigationService.Navigate(new CoursePage(course, this));
        }

        private void GoBackClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new UserPage());
        }
    }
}
