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
using System.Windows.Shapes;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для CoursePage.xaml
    /// </summary>
    public partial class CoursePage : Window
    {
        public delegate void BackClick();
        public event BackClick onBackClick;
        SubscriptionService subscriptionService;
        Course Course { get; set; }

        bool isSubscribe;

        public CoursePage(Course course,BackClick backClick)
        {
            this.Course = course;
            subscriptionService = new SubscriptionService();
            InitializeComponent();
            onBackClick += backClick;
        }

        protected async override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            isSubscribe = (await subscriptionService.CheckIsSubscribe(Course.id)).Data;
            RenderCourse();
        }

        public void RenderCourse()
        {
            name.Text=Course.name;
            description.Text = Course.description;
            authors.ItemsSource = Course.authors;
            RenderSubscribeButton();
        }

        public void RenderSubscribeButton()
        {
            switchSubscribe.Content = isSubscribe ? "Отписаться" : "Подписаться";
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            subscriptionService.Dispose();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            onBackClick();               
            this.Close();
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
            AuthorInfoPage authorInfoPage = new AuthorInfoPage(() =>
            {
                CoursePage coursePage = new CoursePage(Course, this.onBackClick);
                coursePage.Show();
                Close();
            },Course.authors[authors.SelectedIndex].id);
            Close();
            authorInfoPage.Show();
        }
    }
}
