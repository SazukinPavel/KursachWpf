using Kursach.dto;
using Kursach.http;
using System.Threading.Tasks;
using Kursach.Models;

namespace Kursach.Services
{
    class TaskService
    {
        MyHttpClient myHttpClient;
        public TaskService()
        {
            myHttpClient = new MyHttpClient("tasks/");
        }

        public async Task<HttpData<Models.Task>> AddTask(AddTaskDto addTaskDto)
        {
            return await myHttpClient.Post<Models.Task, AddTaskDto>("", addTaskDto);
        }

        public async Task<HttpData<Models.Task[]>> GetUserTasks()
        {
            return await myHttpClient.Get<Models.Task[]>("");
        }

        public async Task<HttpData<Models.Task[]>> GetCourseTasks(string courseId)
        {
            return await myHttpClient.Get<Models.Task[]>(courseId);
        }
        public async Task<HttpData<bool>> UpdateTask( UpdateTaskDto UpdateTaskDto)
        {
            return await myHttpClient.Put<bool,UpdateTaskDto>("",UpdateTaskDto);
        }
        public async Task<HttpData<bool>> DeleteTask( string courseID)
        {
            return await myHttpClient.Delete<bool>(courseID);
        }


    }
}
