namespace Kursach.Types
{
    public class MessageWindowProps
    {
        public string? Header { get; set; }
        public string? Mesasge { get; set; }
        public MessageWindowProps(string header,string message)
        {
            Header = header;
            Mesasge = message;
        }
        public MessageWindowProps(string header)
        {
            Header=header;
        }

    }
}
