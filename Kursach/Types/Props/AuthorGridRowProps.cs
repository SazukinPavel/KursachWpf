using Kursach.Models;

namespace Kursach.Types
{
    public class AuthorGridRowProps
    {
        public delegate void DeleteClick(string id);

        public DeleteClick deleteClick { get; set; }
        public User Author { get; set; }
        public bool IsButtonHide { get; set; }
    }
}
