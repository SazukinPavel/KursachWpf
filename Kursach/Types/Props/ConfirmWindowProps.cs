namespace Kursach.Types
{
    public class ConfirmWindowProps
    {
        public string ConfirmMessage { get; set; }
        public ConfirmWindowProps(string confirmMessage)
        {
            ConfirmMessage=confirmMessage;
        }
    }
}
