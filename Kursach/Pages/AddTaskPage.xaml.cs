using Kursach.dto;
using Kursach.ModalWindows;
using Kursach.Models;
using Kursach.Services;
using Kursach.Types;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Kursach.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddTaskPage.xaml
    /// </summary>
    public partial class AddTaskPage : Page
    {
        TaskService TaskService { get; set; }
        List<Course> MyCourses { get; set; }
        public AddTaskPage(List<Course> courses)
        {
            InitializeComponent();
            MyCourses = courses;
            TaskService = new TaskService();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if(courses.ItemsSource == null)
            {
                courses.ItemsSource = MyCourses;
            }
            base.OnRender(drawingContext);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AuthorPage());
        }

        private async void addButton_Click(object sender, RoutedEventArgs e)
        {
            string titleVallue = title.Text;
            string descVallue = description.Text;
            Course course = courses.SelectedItem as Course;
            if (!string.IsNullOrEmpty(titleVallue) && !string.IsNullOrEmpty(descVallue)&& course!=null)
            {
                var response = await TaskService.AddTask(new AddTaskDto { description = descVallue, title = titleVallue,courseId=course.id });
                if (response.IsSucessfull)
                {
                    ModalWindowFactory.CreateMessageWindow("Задача успешна добавлена").Show();
                }
                else
                {
                    ModalWindowFactory.CreateMessageWindow("Произошла ошибка", response.HttpError.message).Show();
                }
            }
            else
            {
                ModalWindowFactory.CreateMessageWindow("Заполните все поля!").Show();
            }
        }
    }
}
