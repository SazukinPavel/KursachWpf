using Kursach.Controls;
using Kursach.dto;
using Kursach.http;
using Kursach.ModalWindows;
using Kursach.Models;
using Kursach.Services;
using Kursach.Types;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Kursach.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorCoursePage.xaml
    /// </summary>
    public partial class AuthorCoursePage : Page
    {
        Course Course { get; set; }

        OwnCourseService OwnCoursesService { get; set; }

        public AuthorCoursePage(Course course)
        {
            InitializeComponent();
            OwnCoursesService = new OwnCourseService();
            Course = course;
            RenderCourse();
        }

        void RenderCourse()
        {
            name.Text = Course.name;
            description.Text = Course.description;
            RenderAuthors();
        }

        void RenderAuthors()
        {
            authors.Children.Clear();
            var userId = (App.Current as App).User.id;
            foreach (var author in Course.authors)
            {
                AuthorGridRow authorGridRow = new AuthorGridRow(new AuthorGridRowProps { Author= author ,deleteClick=DeleteAuthor,IsButtonHide=author.id==userId});
                authors.Children.Add(authorGridRow);
            }
        }

        async void DeleteAuthor(string id)
        {
            var response=await OwnCoursesService.DeleteAuthorFromCourse(id, Course.id);
            CheckErrorOrExecuteAction(response, () =>
            {
                Course.authors.Remove(Course.authors.Find(authors => authors.id == id));
                RenderAuthors();
            });
        }


        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AuthorPage());
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var savedCourse=new Course { name=name.Text, description=description.Text,id=Course.id};
            var response=await OwnCoursesService.UpdateCourse(savedCourse);
            CheckErrorOrExecuteAction(response, () =>
            {
                Course = new Course { id = Course.id, authors = Course.authors, name = savedCourse.name, description = savedCourse.description };
                RenderCourse();
            });
        }

        private async void addAthorButton_Click(object sender, RoutedEventArgs e)
        {
            var window = ModalWindowFactory.CreateChooseAuthorWindow(Course.authors);
            if ((bool)window.ShowDialog())
            {
                var author = window.ResultAuthor;
                var response=await OwnCoursesService.AddAuthorToCourse(new AddAuthorToCourseDto {authorId =author.id,courseId=Course.id });
                CheckErrorOrExecuteAction(response, () =>
                {
                    Course.authors.Add(new User { id = author.id, role = author.role, name = author.name });
                    RenderAuthors();
                });
            }
        }

        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if((bool)ModalWindowFactory.CreateConfirmWindow("Вы точно хотите удалить жтот курс?").ShowDialog())
            {
                var response= await OwnCoursesService.DeleteCourse(Course.id);
                CheckErrorOrExecuteAction(response, () =>
                {
                    ModalWindowFactory.CreateMessageWindow("Курс был удалён").Show();
                    this.NavigationService.Navigate(new AuthorPage());
                });
            }
        }

        private void CheckErrorOrExecuteAction<T>(HttpData<T> httpData,Action action)
        {
            if (httpData.IsError)
            {
                ModalWindowFactory.CreateMessageWindow("Произошла ошибка", httpData.HttpError.message).Show();
                return;
            }
            action();
        }
    }
}
