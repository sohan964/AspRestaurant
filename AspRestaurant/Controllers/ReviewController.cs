using AspRestaurant.Data;
using AspRestaurant.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet("")]//get all reviews
        public async Task<IActionResult> GetReviews()
        {
            var Reviews = await _reviewRepository.GetReviewsAsync();
            return Ok(Reviews);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddReview([FromBody] Review review)
        {
            var res = await _reviewRepository.AddReviewAsync(review);
            return Ok(res);
        }
    }
}
