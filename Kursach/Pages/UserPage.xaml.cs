using Kursach.Controls;
using Kursach.Models;
using Kursach.Services;
using Kursach.Types;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Kursach.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {

        private List<Course> CoursesList { get; set; }
        private List<Task> TasksList { get; set; }
        SubscriptionService subscriptionService;
        TaskService taskService;
        public UserPage()
        {
            ShowsNavigationUI = false;
            InitializeComponent();
            name.Text = (App.Current as App).User.name;
            subscriptionService = new SubscriptionService();
            taskService = new TaskService();
        }

        async System.Threading.Tasks.Task FetchCourses()
        {
            CoursesList = (await subscriptionService.GetCourses())?.Data?.ToList();
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (CoursesList == null && TasksList==null)
            {
                RenderCoursePanel();
                RenderTaskPanel();
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

        async void RenderTaskPanel()
        {
            await FetchTasks();
            tasks.Children.Clear();
            foreach (var task in TasksList)
            {
                tasks.Children.Add(new TaskCard(new TaskCardProps { Task = task, buttonClick = (t) => { }, ButtonMessage = "Изменить" }));
            }
        }
        async System.Threading.Tasks.Task FetchTasks()
        {
            TasksList = (await taskService.GetUserTasks())?.Data?.ToList();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            AuthService.ResetToken();
            this.NavigationService.Navigate(new StartPage());
        }
    }
}
