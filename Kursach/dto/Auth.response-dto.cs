using Kursach.Models;

namespace Kursach.dto
{
    public class AuthResponseDto
    {
        public string token { get; set; }
        public User user { get; set; }
    }
}
