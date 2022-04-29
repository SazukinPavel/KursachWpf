namespace Kursach.http
{
    public class HttpData<T>
    {
        public bool IsSucessfull { get; set; }
        public T? Data { get; set; }
        public bool IsError { get; set; }
        public HttpError? HttpError { get; set; }
    }
}
