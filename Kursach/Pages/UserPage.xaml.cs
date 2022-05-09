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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {

        private List<Course> CoursesList { get; set; }
        private List<UserTask> TasksList { get; set; }
        private List<Review> ReviewsList { get; set; }

        SubscriptionService subscriptionService;
        TaskService taskService;
        ReviewService reviewService;
        public UserPage()
        {
            ShowsNavigationUI = false;
            InitializeComponent();
            name.Text = (App.Current as App).User.name;
            subscriptionService = new SubscriptionService();
            taskService = new TaskService();
            reviewService= new ReviewService();
        }

        async System.Threading.Tasks.Task FetchCourses()
        {
            CoursesList = (await subscriptionService.GetCourses())?.Data?.ToList();
        }

        async System.Threading.Tasks.Task FetchTasks()
        {
            TasksList = (await taskService.GetUserTasks())?.Data?.ToList();
        }
        async System.Threading.Tasks.Task FetchReviews()
        {
            ReviewsList = (await reviewService.GetReviews())?.Data?.ToList();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (CoursesList == null && TasksList == null && ReviewsList==null)
            {
                RenderCoursePanel();
                RenderTaskPanel();
                RenderReviewPanel();
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

        async void RenderTaskPanel()
        {
            await FetchTasks();
            tasks.Children.Clear();
            foreach (var task in TasksList)
            {
                tasks.Children.Add(new TaskCard(new TaskCardProps
                {
                    Task = task,
                    buttonClick = task.isHaveSolution ? OpenEditSolution : OpenAddSolution,
                    ButtonMessage = task.isHaveSolution ? "Изменить решение" : "Добавить решение"
                }));
            }
        }

        async void RenderReviewPanel()
        {
            await FetchReviews();
            reviews.Children.Clear();
            foreach (var review in ReviewsList)
            {
                reviews.Children.Add(new ReviewCard(new Types.Props.ReviewCardProps { review=review,ButtonMessage="Прочитать",buttonClick=(review)=>OpenReview(review)}));
            }
        }

        public void GoToCoursePage(Course course)
        {
            this.NavigationService.Navigate(new CoursePage(course, new UserPage()));
        }

        void GoToAllCourses()
        {
            this.NavigationService.Navigate(new AllCoursesPage());
        }
        void OpenAddSolution(Task task)
        {
            var window = ModalWindowFactory.CreateAddSolutionWindow(task);
            window.ShowDialog();
            bool needRefetch = (bool)window.DialogResult;
            if (needRefetch)
                RenderTaskPanel();
        }

        void OpenEditSolution(Task task)
        {
            var window = ModalWindowFactory.CreateEditSolutionWindow(task);
            window.ShowDialog();
            bool needRefetch = (bool)window.DialogResult;
            if (needRefetch)
                RenderTaskPanel();
        }

        void OpenReview(Review review)
        {
            var window = ModalWindowFactory.CreateReadReviewWindow(review);
            window.ShowDialog();
            bool needRefetch = (bool)window.DialogResult;
            if (needRefetch)
                RenderReviewPanel();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            AuthService.ResetToken();
            this.NavigationService.Navigate(new StartPage());
        }
    }
}
