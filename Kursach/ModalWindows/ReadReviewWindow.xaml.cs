using Kursach.Models;
using Kursach.Services;
using System.Windows;

namespace Kursach.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для ReadReviewWindwo.xaml
    /// </summary>
    public partial class ReadReviewWindow : Window
    {
        Review Review { get; set; }
        ReviewService reviewService;
        public ReadReviewWindow(Review review)
        {
            InitializeComponent();
            reviewService = new ReviewService();
            Review = review;
            RenderReview();
        }

        void RenderReview()
        {
            taskDescription.Text = Review.solution.task.description;
            taskTitle.Text = Review.solution.task.title;
            review.Text = Review.text;
            solutionText.Text = Review.solution.text;
        }

        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var window = ModalWindowFactory.CreateConfirmWindow("Вы точно хотите удалить ответ автора");
            window.ShowDialog();
            if ((bool)window.DialogResult)
            {
                var response = await reviewService.DeleteReview(Review.id);
                if (response.IsError)
                    ModalWindowFactory.CreateMessageWindow("Произошла ошибка", response.HttpError.message).Show();
                else
                    DialogResult = true;
            }
        }
    }
}
