using Kursach.Controls;
using Kursach.Models;
using Kursach.Services;
using System;
using System.Windows;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для AdminInfoPage.xaml
    /// </summary>
    public partial class AuthorInfoPage : Window
    {
        public delegate void onBack();
        event onBack onBackClick;
        Author Author;
        string AuthorId;
        AuthorService authorService;

        public AuthorInfoPage(onBack onBack, string authorId)
        {
            InitializeComponent();
            onBackClick += onBack;
            authorService = new AuthorService();
            this.AuthorId = authorId;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            onBackClick();
            this.Close();
        }

        public void RenderAuthor()
        {
            name.Text = Author.name;
            bio.Text = Author.bio;
            foreach (var item in Author.Courses)
            {
                courses.Children.Add(new CourseCard(item, onOpenCourse));
            }
        }

        protected async override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            Author = (await authorService.GetAuthorById(AuthorId)).Data;
            RenderAuthor();
        }

        public void onOpenCourse(Course course)
        {
            CoursePage coursePage = new CoursePage(course, () =>
             {
                 AuthorInfoPage authorInfoPage = new AuthorInfoPage(this.onBackClick, this.AuthorId);
                 authorInfoPage.Show();
             });
            this.Close();
        }
    }
}
