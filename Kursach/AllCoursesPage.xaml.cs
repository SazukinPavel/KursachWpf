using Kursach.Controls;
using Kursach.Models;
using Kursach.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для AllCoursesPage.xaml
    /// </summary>
    public partial class AllCoursesPage : Window
    {
        List<Course> Courses;
        CoursesService CoursesService;
        
        public AllCoursesPage()
        {
            InitializeComponent();
            Courses = new List<Course>();
            CoursesService = new CoursesService();
        }

        async System.Threading.Tasks.Task FetchCourses()
        {
            Courses = (await CoursesService.GetCourses()).Data.ToList();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            RenderCoursePanel();
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
            CoursePage coursePage = new CoursePage(course, () =>
            {
                AllCoursesPage allCoursesPage = new AllCoursesPage();
                allCoursesPage.Show();
            });
            Close();
            coursePage.Show();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            CoursesService.Dispose();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserPage userPage = new UserPage();
            this.Close();
            userPage.Show();
        }
    }
}
