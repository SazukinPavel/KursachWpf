using Kursach.dto;
using Kursach.Services;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        string validEmailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        AuthService authService;
        public Register()
        {
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
                    MessageBox.Show("Пароли должны совпадать");
                    return;
                }
                if (!Regex.IsMatch(userEmail, validEmailPattern))
                {
                    MessageBox.Show("Email неккоректный");
                    return;
                }
                var res = await authService.Register(new RegisterDto { email = userEmail, name = username, password = userPassw, role = userRole == "Пользователь" ? "USER" : "AUTHOR" });
                if (res.IsError)
                {
                    MessageBox.Show($"Произошла ошибка:{res.HttpError.message}");
                    return;
                }
                authService.SetAuthorize(res.Data);
                StartWindow startWindow = new StartWindow();
                startWindow.Show();
                Close();
                return;
            }
            MessageBox.Show("Все поля должны быть заполнены");
        }

        private void toLoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            authService.Dispose();
        }
    }
}
