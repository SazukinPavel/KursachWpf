using Kursach.Services;
using Kursach.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        AuthService authService;
        public StartPage()
        {
            ShowsNavigationUI = false;
            InitializeComponent();
            authService = new AuthService();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
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
                        this.NavigationService.Navigate(new UserPage());
                    }
                    else
                    {
                        this.NavigationService.Navigate(new AuthorPage());
                    }
                    return;
                }
                AuthService.ResetToken();
                this.NavigationService.Navigate(new LoginPage());
            }
            catch (HttpRequestException)
            {
                message.Text = "Нет подключения к серверу(";
                refresh.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Authorize();
        }
    }
}
