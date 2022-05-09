using Kursach.Models;

namespace Kursach.Types.Props
{
    public class ReviewCardProps
    {
        public delegate void ButtonClick(Review review);
        public Review review { get; set; }
        public string ButtonMessage { get; set; }
        public ButtonClick buttonClick { get; set; }
    }
}
