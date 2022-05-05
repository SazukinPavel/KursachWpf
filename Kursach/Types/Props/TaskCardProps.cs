using Kursach.Models;

namespace Kursach.Types
{
    public class TaskCardProps
    {
        public delegate void ButtonClick(Task task);
        public Task Task { get; set; }
        public string ButtonMessage { get; set; }
        public ButtonClick buttonClick { get; set; }
        
    }
}
