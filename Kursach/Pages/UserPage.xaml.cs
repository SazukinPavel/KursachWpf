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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {

        private List<Course> CoursesList { get; set; }
        SubscriptionService subscriptionService;
        public UserPage()
        {
            ShowsNavigationUI = false;
            InitializeComponent();
            name.Text = (App.Current as App).User.name;
            subscriptionService = new SubscriptionService();
        }

        async System.Threading.Tasks.Task FetchCourses()
        {
            CoursesList = (await subscriptionService.GetCourses())?.Data?.ToList();
        }


        protected override void OnRender(DrawingContext drawingContext)
        { 
            base.OnRender(drawingContext);
            if(CoursesList == null)
            {
                RenderCoursePanel();
            }
        }

        async void RenderCoursePanel()
        {
            await FetchCourses();
            foreach (var course in CoursesList)
            {
                courses.Children.Add(new CourseCard(course, GoToCoursePage));
            }
            courses.Children.Add(new PlusCard(GoToAllCourses));
        }

        public void GoToCoursePage(Course course)
        {
            this.NavigationService.Navigate(new CoursePage(course, new UserPage()));
        }

        void GoToAllCourses()
        {
            this.NavigationService.Navigate(new AllCoursesPage());
        }

        //void RenderMissionPanel(List<Mission> MissionList)
        //{
        //    //foreach (var mission in MissionList)
        //    //{
        //    //    missions.Children.Add(new MissionCard(mission));
        //    //}
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AuthService.ResetToken();
            this.NavigationService.Navigate(new StartPage());
        }
    }
}
