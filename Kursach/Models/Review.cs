namespace Kursach.Models
{
    public class Review
    {
        public string id { get; set; }
        public string text { get; set; }
        public Solution solution { get; set; }
        public User owner { get; set; }
    }
}
