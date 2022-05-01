using Kursach.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kursach.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorInfoPage.xaml
    /// </summary>
    public partial class AuthorInfoPage : Page
    {
        Author Author;
        string AuthorId;
        AuthorService authorService;
        Page BackPage;

        public AuthorInfoPage(Page backPage, string authorId)
        {
            ShowsNavigationUI = false;
            InitializeComponent();
            authorService = new AuthorService();
            this.AuthorId = authorId;
            BackPage = backPage;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(BackPage);
        }

        public async void RenderAuthor()
        {
            Author = (await authorService.GetAuthorById(AuthorId)).Data;
            name.Text = Author.name;
            bio.Text = Author.bio;
            foreach (var item in Author.Courses)
            {
                courses.Children.Add(new CourseCard(item, onOpenCourse));
            }
        }

        protected  override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if(Author == null)
            {
                RenderAuthor();
            }
        }

        public void onOpenCourse(Course course)
        {
            NavigationService.Navigate(new CoursePage(course,this));
        }
    }
}
