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
using System.Windows.Shapes;

namespace Kursach.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для EditReviewWindow.xaml
    /// </summary>
    public partial class EditReviewWindow : Window
    {
        ReviewService reviewService;
        AuthorSolution Solution { get; }

        public EditReviewWindow(AuthorSolution solution)
        {
            InitializeComponent();
            Solution = solution;
            SetSolution();
            reviewService = new ReviewService();
        }

        void SetSolution()
        {
            taskDescription.Text = Solution.task.description;
            taskTitle.Text = "Задача:" + Solution.task.title;
            solutionText.Text = "Решение:" + Solution.text;
            authorName.Text = "Автор:" + Solution.owner.name;
            review.Text = Solution.review.text;
        }

        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var window = ModalWindowFactory.CreateConfirmWindow("Вы точно хотите удалить ответ автора");
            window.ShowDialog();
            if ((bool)window.DialogResult)
            {
                var response = await reviewService.DeleteReview(Solution.review.id);
                if (response.IsError)
                    ModalWindowFactory.CreateMessageWindow("Произошла ошибка", response.HttpError.message).Show();
                else
                    DialogResult = true;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
