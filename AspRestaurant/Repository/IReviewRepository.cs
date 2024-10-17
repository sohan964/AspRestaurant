using AspRestaurant.Data;

namespace AspRestaurant.Repository
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetReviewsAsync();
        Task<Review> AddReviewAsync(Review review);
    }
}
