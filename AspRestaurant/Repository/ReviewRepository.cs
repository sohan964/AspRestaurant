using AspRestaurant.Data;
using Microsoft.EntityFrameworkCore;

namespace AspRestaurant.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AspRestaurantContext _context;

        public ReviewRepository(AspRestaurantContext context)
        {
            _context = context;
        }

        //GetAllReviews
        public async Task<List<Review>> GetReviewsAsync()
        {
            var Reviews = await _context.Reviews.Select(review => new Review
            {
                Id = review.Id,
                Name = review.Name,
                Details = review.Details,
                Rating = review.Rating,
            }).ToListAsync();
            return Reviews;
        }
    }
}
