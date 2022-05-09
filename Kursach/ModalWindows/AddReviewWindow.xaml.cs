using Kursach.dto.Review;
using Kursach.Models;
using Kursach.Services;
using System.Windows;

namespace Kursach.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для AddReviewWindow.xaml
    /// </summary>
    public partial class AddReviewWindow : Window
    {
        Solution Solution { get; set; }
        ReviewService reviewService;
        public AddReviewWindow(Solution solution)
        {
            InitializeComponent();
            Solution = solution;
            SetSolution();
            reviewService= new ReviewService();
        }

        void SetSolution()
        {
            taskDescription.Text =Solution.task.description;
            taskTitle.Text = "Задача:" + Solution.task.title;
            solutionText.Text ="Решение:"+ Solution.text;
            authorName.Text = "Автор:" + Solution.owner.name;
        }

        private async void addReview_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(review.Text))
            {
                var response= await reviewService.AddReview(new AddReviewDto { solutionId = Solution.id, text = review.Text });
                if (response.IsSucessfull)
                {
                    DialogResult = true;
                }
                else
                {
                    ModalWindowFactory.CreateMessageWindow("Произошла ошибка",response.HttpError.message);
                }
            }
            ModalWindowFactory.CreateMessageWindow("Заполните поле ваш ответ");
        }
    }
}
