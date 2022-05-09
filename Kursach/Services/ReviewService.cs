using Kursach.dto.Review;
using Kursach.http;
using Kursach.Models;
using System.Threading.Tasks;

namespace Kursach.Services
{
    public class ReviewService
    {
        MyHttpClient myHttpClient;
        public ReviewService()
        {
            myHttpClient = new MyHttpClient("reviews/");
        }

        public async Task<HttpData<bool>> AddReview(AddReviewDto addReviewDto)
        {
            return await myHttpClient.Post<bool, AddReviewDto>("", addReviewDto);
        }

        public async Task<HttpData<Review[]>> GetReviews()
        {
            return await myHttpClient.Get<Review[]>("");
        }

        public async Task<HttpData<bool>> UpdateSolution(UpdateReviewDto updateReviewDto)
        {
            return await myHttpClient.Put<bool, UpdateReviewDto>("", updateReviewDto);
        }

        public async Task<HttpData<Review>> GetReviewById(string reviewId)
        {
            return await myHttpClient.Get<Review>(reviewId);
        }

        public async Task<HttpData<bool>> DeleteReview(string reviewId)
        {
            return await myHttpClient.Delete<bool>(reviewId);
        }
    }
}
