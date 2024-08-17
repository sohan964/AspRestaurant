using AspRestaurant.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetCategories()
        {
            var allCategories = await _categoryRepository.GetAllCategoryAsync();
            return Ok(allCategories);
        }
    }
}
