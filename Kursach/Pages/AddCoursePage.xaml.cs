using Kursach.dto;
using Kursach.ModalWindows;
using Kursach.Services;
using Kursach.Types;
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
    /// Логика взаимодействия для AddCoursePage.xaml
    /// </summary>
    public partial class AddCoursePage : Page
    {

        OwnCourseService OwnCoursesService { get; set; }
        public AddCoursePage()
        {
            InitializeComponent();
            OwnCoursesService = new OwnCourseService();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AuthorPage());
        }

        private async void addButton_Click(object sender, RoutedEventArgs e)
        {
            string nameVallue = name.Text;
            string descripptionVallue = description.Text;
            if (!string.IsNullOrEmpty(nameVallue) && !string.IsNullOrEmpty(descripptionVallue))
            {
                var response = await OwnCoursesService.AddCourse(new AddCourseDto { description = descripptionVallue, name = nameVallue });
                if (response.IsSucessfull)
                {
                    ModalWindowFactory.CreateMessageWindow(new MessageWindowProps("Курс был добавлен")).Show();
                }
                else
                {
                    ModalWindowFactory.CreateMessageWindow(new MessageWindowProps("Произошла ошибка",response.HttpError.message)).Show();
                }
            }
            ModalWindowFactory.CreateMessageWindow(new MessageWindowProps("Заполните все поля")).Show();
        }
    }
}
