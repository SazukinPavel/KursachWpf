using Kursach.Controls;
using Kursach.http;
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

namespace Kursach
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Window
    {

        private List<Course> CoursesList { get; set; }
        SubscriptionService subscriptionService;
        public UserPage()
        {
            InitializeComponent();
            subscriptionService = new SubscriptionService();
        }

        async System.Threading.Tasks.Task FetchCourses()
        {
            CoursesList = (await subscriptionService.GetCourses())?.Data?.ToList();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            RenderCoursePanel();
        }

        async void RenderCoursePanel()
        {
            await FetchCourses();
            foreach (var course in CoursesList)
            {
                courses.Children.Add(new CourseCard(course,GoToCoursePage));                
            }
            courses.Children.Add(new PlusCard(GoToAllCourses));
        }

        public void GoToCoursePage(Course course)
        {
            CoursePage coursePage = new CoursePage(course, () =>
            {
                UserPage userPage = new UserPage();
                userPage.Show();
            });
            this.Close();
            coursePage.Show();
        }

        void GoToAllCourses()
        {
            AllCoursesPage allCoursesPage = new AllCoursesPage();
            this.Close();
            allCoursesPage.Show();
        }

        //void RenderMissionPanel(List<Mission> MissionList)
        //{
        //    //foreach (var mission in MissionList)
        //    //{
        //    //    missions.Children.Add(new MissionCard(mission));
        //    //}
        //}

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            subscriptionService.Dispose();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AuthService.ResetToken();
            StartWindow startWindow = new StartWindow();
            this.Close();
            startWindow.Show();
        }
    }
}
