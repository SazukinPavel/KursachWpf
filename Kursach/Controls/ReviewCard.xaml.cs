using Kursach.Models;
using Kursach.Types.Props;
using System.Windows.Controls;
using System.Windows.Media;

namespace Kursach.Controls
{
    /// <summary>
    /// Логика взаимодействия для ReviewCard.xaml
    /// </summary>
    public partial class ReviewCard : UserControl
    {
        Review Review { get; set; }

        public ReviewCard(ReviewCardProps reviewCardProps)
        {
            InitializeComponent();
            SetProps(reviewCardProps);
            RenderReviewCard();
        }

        void SetProps(ReviewCardProps reviewCardProps)
        {
            Review = reviewCardProps.review;
            border.BorderBrush = Review.isRight ? Brushes.Green:Brushes.Red;
            button.Content = reviewCardProps.ButtonMessage;
            button.Click += (sender,e)=>reviewCardProps.buttonClick(Review);
        }

        void RenderReviewCard()
        {
            taskName.Text =Review.solution.task.title;
            course.Text = "Курс:" + Review.solution.task.course.name;
        }

    }
}
