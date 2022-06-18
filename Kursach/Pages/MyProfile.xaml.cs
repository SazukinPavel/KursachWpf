using Kursach.ModalWindows;
using Kursach.Services;
using System.Windows;
using System.Windows.Controls;

namespace Kursach.Pages
{
    /// <summary>
    /// Логика взаимодействия для MyProfile.xaml
    /// </summary>
    public partial class MyProfile : Page
    {
        MyProfileService MyProfileService { get; set; }

        public MyProfile()
        {
            InitializeComponent();
            RenderProfile();
            MyProfileService = new MyProfileService();
            if((App.Current as App).User.role == "AUTHOR")
            {
                setBioButton();
            }
        }

        void setBioButton()
        {
            var button = new Button { Content = "Изменить био" };
            button.Click += updateWindow;
            buttons.Children.Add(button);
        }

        private void updateWindow(object sender, RoutedEventArgs e)
        {
            ModalWindowFactory.CreateUpdateBioWindow().ShowDialog();
        }



        void RenderProfile()
        {
            var user = (App.Current as App).User;
            name.Text = user.name;
            email.Text = user.email;
            role.Text = user.role=="AUTHOR"?"Автор":"Пользователь";
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void changePassword_Click(object sender, RoutedEventArgs e)
        {
            ModalWindowFactory.CreateChangePasswordWindow(MyProfileService).ShowDialog();
        }

        private async void save_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(name.Text))
            {
                var res = await MyProfileService.UpdateName(new dto.Review.UpdateNameDto { name = name.Text });
                if (res.IsError)
                {
                    ModalWindowFactory.CreateMessageWindow($"Произошла ошибка: {res.HttpError.message}").Show();
                    return;
                }
                (App.Current as App).User.name = name.Text;
                RenderProfile();
            }
            else
            {
                ModalWindowFactory.CreateMessageWindow("Имя не может быть пустым").Show();
            }
        }
    }
}
