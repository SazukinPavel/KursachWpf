using Kursach.dto;
using Kursach.http;
using Kursach.Models;
using System.Threading.Tasks;

namespace Kursach.Services
{
    public class AuthorService
    {
        MyHttpClient myHttpClient;
        public AuthorService()
        {
            myHttpClient = new MyHttpClient("authors/");
        }

        public async Task<HttpData<Author[]>> GetAuthors()
        {
            return await myHttpClient.Get<Author[]>("");
        }

        public async Task<HttpData<Author>> GetAuthorById(string id)
        {
            return await myHttpClient.Get<Author>(id);
        }

        public async Task<HttpData<Author>> UpdateAuthorBio(string bio)
        {
            return await myHttpClient.Put<Author, UpdateAuthorDto>("",new UpdateAuthorDto { bio=bio});
        }

    }
}
