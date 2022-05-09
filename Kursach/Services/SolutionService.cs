using Kursach.dto;
using Kursach.dto.Solution;
using Kursach.http;
using Kursach.Models;
using System.Threading.Tasks;

namespace Kursach.Services
{
    public class SolutionService
    {
        MyHttpClient myHttpClient;
        public SolutionService()
        {
            myHttpClient = new MyHttpClient("solutions/");
        }

        public async Task<HttpData<bool>> AddSolution(AddSolutionDto addSolutionDto)
        {
            return await myHttpClient.Post<bool, AddSolutionDto>("", addSolutionDto);
        }

        public async Task<HttpData<Solution[]>> GetSolutions()
        {
            return await myHttpClient.Get<Solution[]>("");
        }

        public async Task<HttpData<AuthorSolution[]>> GetAuthorSolutions()
        {
            return await myHttpClient.Get<AuthorSolution[]>("");
        }

        public async Task<HttpData<bool>> UpdateSolution(UpdateSolutionDto updateSolutionDto)
        {
            return await myHttpClient.Put<bool,UpdateSolutionDto>("", updateSolutionDto);
        }

        public async Task<HttpData<Solution>> GetSolutionByTaskId(string taskId)
        {
            return await myHttpClient.Get<Solution>(taskId);
        }

        public async Task<HttpData<bool>> DeleteSolution(string solutionId)
        {
            return await myHttpClient.Delete<bool>(solutionId);
        }
    }
}
