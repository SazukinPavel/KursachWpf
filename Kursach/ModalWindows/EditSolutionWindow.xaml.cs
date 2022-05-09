using Kursach.dto.Solution;
using Kursach.Models;
using Kursach.Services;
using Kursach.Types;
using System.Windows;

namespace Kursach.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для EditSolutionWindow.xaml
    /// </summary>
    public partial class EditSolutionWindow : Window
    {
        Task Task { get; set; }
        Solution Solution { get; set; }
        SolutionService solutionService;


        public EditSolutionWindow(Task task)
        {
            InitializeComponent();
            solutionService = new SolutionService();
            Task = task;
            Render();
        }

        async System.Threading.Tasks.Task FetchSolution()
        {
            Solution = (await solutionService.GetSolutionByTaskId(Task.id)).Data;
        }

        void RenderSolution()
        {
            solution.Text = Solution.text;
        }

        void RenderTask()
        {
            taskText.Text = Task.description;
            taskTitle.Text = Task.title;
        }

        async void Render()
        {
            await FetchSolution();
            RenderTask();
            RenderSolution();
        }
        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(solution.Text))
            {
                var response = await solutionService.UpdateSolution(new UpdateSolutionDto { solutionId = Solution.id, text = solution.Text });
                if (response.IsError)
                    ModalWindowFactory.CreateMessageWindow("Произошла ошибка", response.HttpError.message).Show();
                else
                    DialogResult = true;
            }
            else
                ModalWindowFactory.CreateMessageWindow("Решение не может быть пустым").Show();
        }

        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var window = ModalWindowFactory.CreateConfirmWindow("Вы точно хотите удалить решение?");
            window.ShowDialog();
            if ((bool)window.DialogResult)
            {
                var response = await solutionService.DeleteSolution(Solution.id);
                if (response.IsError)
                    ModalWindowFactory.CreateMessageWindow("Произошла ошибка", response.HttpError.message).Show();
                else
                    DialogResult = true;
            }
        }
    }
}

