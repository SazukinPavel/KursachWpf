using Kursach.http;
using Kursach.Models;
using System.Threading.Tasks;

namespace Kursach.Services
{
    internal class OwnCoursesService
    {
        private MyHttpClient myHttpClient;

        public OwnCoursesService()
        {
            myHttpClient = new MyHttpClient("own-courses/");
        }

        public async Task<HttpData<Course[]>> GetCourses()
        {
            return await myHttpClient.Get<Course[]>("");
        }

        public async Task<HttpData<Course>> GetCourseById(string id)
        {
            return await myHttpClient.Get<Course>(id);
        }

        public void Dispose()
        {
            myHttpClient.Dispose();
        }
    }
}
