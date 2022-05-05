using Kursach.http;
using Kursach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.Services
{
    internal class CourseService
    {

        MyHttpClient myHttpClient;

        public CourseService()
        {
            myHttpClient = new MyHttpClient("courses/");
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
