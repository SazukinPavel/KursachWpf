using Kursach.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kursach.Pages
{
    /// <summary>
    /// Логика взаимодействия для CoursePage.xaml
    /// </summary>
    public partial class CoursePage : Page
    {
        public delegate void BackClick();
        public event BackClick onBackClick;
        SubscriptionService subscriptionService;
        Course Course { get; set; }
        Page goBack;

        bool isSubscribe;

        public CoursePage(Course course,Page goBack)
        {
            ShowsNavigationUI = false;
            InitializeComponent();
            this.Course = course;
            this.goBack=goBack;
            subscriptionService = new SubscriptionService();
        }

        protected async override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            isSubscribe = (await subscriptionService.CheckIsSubscribe(Course.id)).Data;
            RenderCourse();
        }

        public void RenderCourse()
        {
            name.Text = Course.name;
            description.Text = Course.description;
            authors.ItemsSource = Course.authors;
            RenderSubscribeButton();
        }

        public void RenderSubscribeButton()
        {
            switchSubscribe.Content = isSubscribe ? "Отписаться" : "Подписаться";
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(goBack);
        }

        private async void switchSubscribe_Click(object sender, RoutedEventArgs e)
        {
            if (isSubscribe)
            {
                await subscriptionService.DeleteSubscribe(Course.id);
            }
            else
            {
                await subscriptionService.AddSubscribe(Course.id);
            }
            isSubscribe = !isSubscribe;
            RenderSubscribeButton();
        }

        private void authors_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AuthorInfoPage authorInfoPage = new AuthorInfoPage(this, Course.authors[authors.SelectedIndex].id);
            this.NavigationService.Navigate(authorInfoPage);
        }
    }
}
