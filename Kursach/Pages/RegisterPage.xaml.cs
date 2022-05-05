using Kursach.dto;
using Kursach.ModalWindows;
using Kursach.Services;
using Kursach.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        string validEmailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        AuthService authService;
        public RegisterPage()
        {
            ShowsNavigationUI = false;
            InitializeComponent();
            authService = new AuthService();
            role.ItemsSource = new List<string> { "Пользователь", "Автор" };
            role.SelectedIndex = 0;
        }

        private async void registerButton_Click(object sender, RoutedEventArgs e)
        {
            string username = name.Text;
            string userPassw = password.Text;
            string repeatPassw = repeatPassword.Text;
            string userEmail = email.Text;
            string userRole = role.Text;
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(userPassw) && !string.IsNullOrEmpty(repeatPassw)
                && !string.IsNullOrEmpty(userEmail) && !string.IsNullOrEmpty(userRole))
            {
                if (userPassw != repeatPassw)
                {
                    ModalWindowFactory.CreateMessageWindow(new MessageWindowProps("Пароли должны совпадать")).Show();
                    return;
                }
                if (!Regex.IsMatch(userEmail, validEmailPattern))
                {
                    ModalWindowFactory.CreateMessageWindow(new MessageWindowProps("Email неккоректный")).Show();
                    return;
                }
                var res = await authService.Register(new RegisterDto { email = userEmail, name = username, password = userPassw, role = userRole == "Пользователь" ? "USER" : "AUTHOR" });
                if (res.IsError)
                {
                    ModalWindowFactory.CreateMessageWindow(new MessageWindowProps("Произошла ошибка",res.HttpError.message)).Show();
                    return;
                }
                authService.SetAuthorize(res.Data);
                this.NavigationService.Navigate(new StartPage());
                return;
            }
            ModalWindowFactory.CreateMessageWindow(new MessageWindowProps("Все поля должны быть заполнены")).Show();
        }

        private void toLoginButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoginPage());
        }

    }
}
