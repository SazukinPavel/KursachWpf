using Kursach.dto;
using Kursach.http;
using Kursach.Models;
using System.Threading.Tasks;

namespace Kursach.Services
{
    public class SubscriptionService
    {
        private MyHttpClient myHttpClient;

        public SubscriptionService()
        {
            myHttpClient = new MyHttpClient("subscriptions/");
        }

        public async Task<HttpData<Course[]>> GetCourses()
        {
            return await myHttpClient.Get<Course[]>("");
        }

        public async Task<HttpData<Course>> AddSubscribe(string courseId)
        {
            return await myHttpClient.Post<Course,AddSubscribeDto>("", new AddSubscribeDto { courseId = courseId });
        }

        public async Task<HttpData<bool>> DeleteSubscribe(string courseId)
        {
            return await myHttpClient.Delete<bool>(courseId);
        }

        public async Task<HttpData<bool>> CheckIsSubscribe(string courseId)
        {
            return await myHttpClient.Get<bool>(courseId);
        }

        public void Dispose()
        {
            myHttpClient.Dispose();
        }
    }
}
