using Kursach.Controls;
using Kursach.ModalWindows;
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
    /// Логика взаимодействия для AuthorPage.xaml
    /// </summary>
    public partial class AuthorPage : Page
    {
        private List<Course> CoursesList { get; set; }
        private List<Models.Task> TasksList { get; set; }
        OwnCourseService ownCoursesService;
        TaskService taskService;
        public AuthorPage()
        {
            ShowsNavigationUI = false;
            InitializeComponent();
            name.Text = (App.Current as App).User.name;
            ownCoursesService = new OwnCourseService();
            taskService = new TaskService();
        }
        async System.Threading.Tasks.Task FetchCourses()
        {
            CoursesList = (await ownCoursesService.GetCourses())?.Data?.ToList();
        }
        async System.Threading.Tasks.Task FetchTasks()
        {
            TasksList = (await taskService.GetUserTasks())?.Data?.ToList();
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (CoursesList == null && TasksList == null)
            {
                RenderCoursePanel();
                RenderTaskPanel();
            }
        }
        async void RenderTaskPanel()
        {
            await FetchTasks();
            tasks.Children.Clear();
            foreach (var task in TasksList)
            {
                tasks.Children.Add(new TaskCard(new TaskCardProps { Task=task,buttonClick=EditCourse,ButtonMessage="Изменить"}));
            }
        }

        async void RenderCoursePanel()
        {
            await FetchCourses();
            tasks.Children.Clear();
            foreach (var course in CoursesList)
            {
                courses.Children.Add(new CourseCard(course, GoToCoursePage));
            }
            courses.Children.Add(new PlusCard(GoToAddCourse));
        }

        void GoToCoursePage(Course course)
        {
            this.NavigationService.Navigate(new AuthorCoursePage(course));
        }

        void EditCourse(Task task)
        {
            ModalWindowFactory.CreateEditTaskWindow(task).ShowDialog();
            RenderTaskPanel();
        }

        void GoToAddCourse()
        {
            this.NavigationService.Navigate(new AddCoursePage());
        }
        void exitButton_Click(object sender, RoutedEventArgs e)
        {
            AuthService.ResetToken();
            this.NavigationService.Navigate(new StartPage());
        }

        void addTaskButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddTaskPage(CoursesList));
        }
    }
}
