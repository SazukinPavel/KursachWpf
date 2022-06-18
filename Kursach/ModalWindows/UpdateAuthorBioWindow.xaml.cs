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
    /// Логика взаимодействия для UpdateAuthorBioWindow.xaml
    /// </summary>
    public partial class UpdateAuthorBioWindow : Window
    {
        AuthorService authorService;
        public UpdateAuthorBioWindow()
        {
            InitializeComponent();
            authorService = new AuthorService();
            SetAuthorBio();
        }

        async void SetAuthorBio()
        {
            bio.Text=(await authorService.GetAuthorById((App.Current as App).User.id))?.Data?.bio ?? "";
        }

        private async void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var res = await authorService.UpdateAuthorBio(bio.Text);
            if (res.IsError)
            {
                ModalWindowFactory.CreateMessageWindow($"Что то пошло не так:{res.HttpError.message}").Show();
            }else
                ModalWindowFactory.CreateMessageWindow($"Био изменено").Show();

        }
    }
}
