using Kursach.dto;
using Kursach.http;
using Kursach.Models;
using System.Threading.Tasks;

namespace Kursach.Services
{
    internal class OwnCourseService
    {
        private MyHttpClient myHttpClient;

        public OwnCourseService()
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

        public async Task<HttpData<bool>> UpdateCourse(Course course)
        {
            return await myHttpClient.Put<bool, Course>("", course);
        }

        public async Task<HttpData<bool>> DeleteAuthorFromCourse(string authorId,string courseId)
        {
            return await myHttpClient.Delete<bool>($"{courseId}/{authorId}");
        }

        public async Task<HttpData<bool>> DeleteCourse(string courseId)
        {
            return await myHttpClient.Delete<bool>(courseId);
        }

        public async Task<HttpData<bool>> AddAuthorToCourse(AddAuthorToCourseDto addAuthorToCourseDto)
        {
            return await myHttpClient.Post<bool, AddAuthorToCourseDto>("authors", addAuthorToCourseDto);
        }
        public async Task<HttpData<Course>> AddCourse(AddCourseDto addCourseDto)
        {
            return await myHttpClient.Post<Course, AddCourseDto>("", addCourseDto);
        }

        public void Dispose()
        {
            myHttpClient.Dispose();
        }
    }
}
