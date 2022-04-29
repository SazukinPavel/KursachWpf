using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Models
{
    public class User
    {
        public string id { get; set; }
        public string name { get; set; }
        public string role { get; set; }

        public override string ToString()
        {
            return $"{name}";
        }
    }
}
