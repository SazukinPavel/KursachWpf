using Kursach.Models;
using Kursach.Services;
using Kursach.Types;
using System.Windows;

namespace Kursach.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        Task Task { get; set; }
        TaskService TaskService { get; set; }

        public EditTaskWindow( Task task)
        {
            InitializeComponent();
            Task= task;
            RenderTask();
            TaskService = new TaskService();
        }

        void RenderTask()
        {
            title.Text = Task.title;
            description.Text=Task.description;
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(title.Text) && !string.IsNullOrEmpty(description.Text))
            {
                var response = await TaskService.UpdateTask(new dto.UpdateTaskDto { description = description.Text, title = title.Text, taskId = Task.id });
                if(response.IsError)
                {
                    ModalWindowFactory.CreateMessageWindow("Произошла ошибка", response.HttpError.message).Show();
                }
            }
        }

        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var modal = ModalWindowFactory.CreateConfirmWindow("Вы точно хотите удалить эту задачу?");
            modal.ShowDialog();
            if ((bool)modal.DialogResult)
            {
                var response=await TaskService.DeleteTask(Task.id);
                if (response.IsSucessfull)
                {
                    DialogResult = true;
                    ModalWindowFactory.CreateMessageWindow("Задача успешна удалена").Show();
                }
                else
                {
                    ModalWindowFactory.CreateMessageWindow("Произошла ошибка", response.HttpError.message).Show();
                }
            }
        }
    }
}
