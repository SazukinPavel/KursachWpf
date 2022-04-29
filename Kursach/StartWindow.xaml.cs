using Kursach.Services;
using Kursach.Types;
using Microsoft.Win32;
using System;
using System.Net.Http;
using System.Windows;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        AuthService authService;
        public StartWindow()
        {
            InitializeComponent();
            authService = new AuthService();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            Authorize();
        }

        private async void Authorize()
        {
            refresh.Visibility = Visibility.Hidden;
            message.Text = "Подождите...\nИдёт проверка авторизации.";
            try
            {
                var user = await authService.Authorize();
                if (user != null)
                {
                    (Application.Current as App).User = user;
                    if (user.role == RoleType.GetUser())
                    {
                        UserPage userPage = new UserPage();
                        userPage.Show();
                    }
                    else
                    {
                        AuthorPage authorPage = new AuthorPage();
                        authorPage.Show();
                    }
                    this.Close();
                    return;
                }
                AuthService.ResetToken();
                Login login = new Login();
                login.Show();
                this.Close();
            }
            catch (HttpRequestException)
            {
                message.Text = "Нет подключения к серверу(";
                refresh.Visibility=Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Authorize();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            authService.Dispose();
        }

    }
}
