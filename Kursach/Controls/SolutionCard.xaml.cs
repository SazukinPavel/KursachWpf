using Kursach.Models;
using Kursach.Types.Props;
using System.Windows;
using System.Windows.Controls;

namespace Kursach.Controls
{
    /// <summary>
    /// Логика взаимодействия для SolutionCard.xaml
    /// </summary>
    public partial class SolutionCard : UserControl
    {
        AuthorSolution Solution { get; set; }
        public SolutionCard(SolutionCardProps solutionCardProps)
        {
            InitializeComponent();
            SetProps(solutionCardProps);
            RenderSolutionCard();
        }

        void SetProps(SolutionCardProps solutionCardProps)
        {
            Solution = solutionCardProps.solution;
            button.Content = solutionCardProps.ButtonMessage;
            button.Click += (r, e) => solutionCardProps.buttonClick(Solution);
        }

        void RenderSolutionCard()
        {
            text.Text = Solution.text.Length<12?Solution.text:Solution.text.Substring(0,11);
            name.Text = Solution.task.title;
            course.Text = $"Курс:{Solution.task.course.name}";
            author.Text = $"Автор:{Solution.owner.name}";
        }

        
    }
}
