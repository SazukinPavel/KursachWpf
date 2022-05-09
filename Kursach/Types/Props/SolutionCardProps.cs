
using Kursach.Models;

namespace Kursach.Types.Props
{
    public class SolutionCardProps
    {
        public delegate void ButtonClick(AuthorSolution solution);
        public AuthorSolution solution { get; set; }
        public string ButtonMessage { get; set; }
        public ButtonClick buttonClick { get; set; }
    }
}
