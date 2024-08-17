using AspRestaurant.Data;
using Microsoft.EntityFrameworkCore;

namespace AspRestaurant.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AspRestaurantContext _context;

        public CategoryRepository(AspRestaurantContext context)
        {
            _context = context;
        }

        //getall category
        public async Task<List<Category>> GetAllCategoryAsync()
        {
            var categories = await _context.Categories.Select(category => new Category()
            {
                Id = category.Id,
                Name = category.Name,
            }).ToListAsync();
            return categories;
        }
    }
}
