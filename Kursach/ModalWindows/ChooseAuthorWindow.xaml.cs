using Kursach.Models;
using Kursach.Services;
using Kursach.Types;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Kursach.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для ChooseAuthorWindow.xaml
    /// </summary>
    public partial class ChooseAuthorWindow : Window
    {

        List<Author> authorList;
        List<User> oldAuthors;
        public Author ResultAuthor;

        AuthorService AuthorService { get; set; }

        public ChooseAuthorWindow(List<User> users)
        {
            InitializeComponent();
            AuthorService = new AuthorService();
            oldAuthors = users;
        }

        protected async override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            FetchAuthors();
        }

        async System.Threading.Tasks.Task FetchAuthors()
        {
            var authorsD = await AuthorService.GetAuthors();
            authorList = authorsD.Data.Where((author) => !oldAuthors.Exists(u => u.id == author.id)).Select(i => i).ToList();
            authors.ItemsSource = authorList;
        }

        private void addAuthor_Click(object sender, RoutedEventArgs e)
        {
            if (authors.SelectedItem == null)
            {
                ModalWindowFactory.CreateMessageWindow("Выберите автора!").Show();
                return;
            }
            ResultAuthor = authors.SelectedItem as Author;
            DialogResult = true;
        }
    }
}
