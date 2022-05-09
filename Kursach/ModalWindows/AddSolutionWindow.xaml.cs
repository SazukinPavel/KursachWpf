using Kursach.dto;
using Kursach.Models;
using Kursach.Services;
using Kursach.Types;
using System.Windows;

namespace Kursach.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для AddSolutionWindow.xaml
    /// </summary>
    public partial class AddSolutionWindow : Window
    {
        Task Task { get; set; }
        SolutionService solutionService;

        public AddSolutionWindow(Task task)
        {
            InitializeComponent();
            solutionService = new SolutionService();
            Task = task;
            RenderTask();
        }

        void RenderTask()
        {
            taskText.Text = Task.description;
            taskTitle.Text = Task.title;
        }

        private async void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(solution.Text))
            {
                var response =await solutionService.AddSolution(new AddSolutionDto { taskId=Task.id,text=solution.Text});
                if (response.IsSucessfull)
                {
                    DialogResult = true;
                    ModalWindowFactory.CreateMessageWindow("Решение успешно добавлено").Show();
                }
                else
                    ModalWindowFactory.CreateMessageWindow("Произошла ошибка",response.HttpError.message).Show();
            }
            else
                ModalWindowFactory.CreateMessageWindow( "Напишите ваше решение").Show();
        }
    }
}
