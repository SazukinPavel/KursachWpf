using Kursach.dto;
using Kursach.Services;
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
using System.Windows.Shapes;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        string validEmailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        AuthService authService;
        public Login()
        {
            InitializeComponent();
            authService = new AuthService();
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string usernameOrEmail = nameOrEmail.Text;
            string userPassw = password.GetPassword();
            if (!string.IsNullOrEmpty(usernameOrEmail) && !string.IsNullOrEmpty(userPassw))
            {
                var res = await authService.Login(new LoginDto { emailOrName = usernameOrEmail,password=userPassw});
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

        private void onRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            authService.Dispose();
        }
    }
}
