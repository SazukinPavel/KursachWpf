using System.Collections.Generic;

namespace Kursach.Models
{
    public class Course
    {
        public string id { get; init; }
        public string name { get; init; }
        public string description { get; init; }
        public List<User>? authors { get; init; }
    }
}
