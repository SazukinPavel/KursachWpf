using Kursach.Controls;
using Kursach.ModalWindows;
using Kursach.Models;
using Kursach.Services;
using Kursach.Types;
using Kursach.Types.Props;
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
        private List<AuthorTask> TasksList { get; set; }
        private List<AuthorSolution> SolutionList { get; set; }

        OwnCourseService ownCoursesService;
        TaskService taskService;
        SolutionService solutionService;
        public AuthorPage()
        {
            ShowsNavigationUI = false;
            InitializeComponent();
            name.Text = (App.Current as App).User.name;
            ownCoursesService = new OwnCourseService();
            taskService = new TaskService();
            solutionService=new SolutionService();
        }
        async System.Threading.Tasks.Task FetchCourses()
        {
            CoursesList = (await ownCoursesService.GetCourses())?.Data?.ToList();
        }
        async System.Threading.Tasks.Task FetchTasks()
        {
            TasksList = (await taskService.GetAuthorTasks())?.Data?.ToList();
        }
        async System.Threading.Tasks.Task FetchSolutions()
        {
            SolutionList = (await solutionService.GetAuthorSolutions())?.Data?.ToList();
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (CoursesList == null && TasksList == null && SolutionList==null)
            {
                RenderCoursePanel();
                RenderTaskPanel();
                RenderSolutionsPanel();
            }
        }
        async void RenderTaskPanel()
        {
            await FetchTasks();
            tasks.Children.Clear();
            foreach (var task in TasksList)
            {
                tasks.Children.Add(new TaskCard(new TaskCardProps { Task = task, buttonClick = EditCourse, ButtonMessage = "Изменить" }));
            }
        }

        async void RenderCoursePanel()
        {
            await FetchCourses();
            courses.Children.Clear();
            foreach (var course in CoursesList)
            {
                courses.Children.Add(new CourseCard(course, GoToCoursePage));
            }
            courses.Children.Add(new PlusCard(GoToAddCourse));
        }

        async void RenderSolutionsPanel()
        {
            await FetchSolutions();
            solutions.Children.Clear();
            foreach (var solution in SolutionList)
            {
                solutions.Children.Add(new SolutionCard(new SolutionCardProps { buttonClick= solution.isHaveReview? OnEditReview:OnAddReview,
                    solution=solution, ButtonMessage=solution.isHaveReview?"Изменить":"Ответить"}));
            }
        }

        void OnAddReview(AuthorSolution solution)
        {
            var window=ModalWindowFactory.CreateAddReviewWindow(solution);
            window.ShowDialog();
            if ((bool)window.DialogResult)
            {
                RenderSolutionsPanel();
            }
        }

        void OnEditReview(AuthorSolution solution)
        {
            var window = ModalWindowFactory.CreateEditSolutionWindow(solution);
            window.ShowDialog();
            if ((bool)window.DialogResult)
            {
                RenderSolutionsPanel();
            }
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
