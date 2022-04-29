using Kursach.Types;
using System.Collections.Generic;

namespace Kursach.Models
{
    public class Author:User
    {
        public Author()
        {
            role = RoleType.GetAuthor();
        }

        public string bio { get; set; }
        public List<Course> Courses { get; set; }

    }
}
