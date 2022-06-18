using Kursach.Services;
using System.Windows;

namespace Kursach.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        MyProfileService profileService;
        public ChangePasswordWindow(MyProfileService myProfileService)
        {
            InitializeComponent();
            profileService = myProfileService;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void edit_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(newPassword.Text) && !string.IsNullOrEmpty(oldPassword.Text) && newPassword.Text.Length>=8
                && oldPassword.Text.Length >=8)
            {
                var res=await profileService.UpdatePassword(new dto.Review.UpdatePasswordDto { newPassword = newPassword.Text,oldPassword=oldPassword.Text});
                if (res.IsError)
                {
                    ModalWindowFactory.CreateMessageWindow($"Произошла ошибка:{res.HttpError.message}").Show();
                    return;
                }
                 ModalWindowFactory.CreateMessageWindow("Пароль успешно изменён").Show();

            }
            else
            {
                ModalWindowFactory.CreateMessageWindow("Пароль не может быть пустым или меньше 8 символов").Show();
            }
        }
    }
}
