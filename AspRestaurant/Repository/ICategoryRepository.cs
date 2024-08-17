using AspRestaurant.Data;

namespace AspRestaurant.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoryAsync();
    }
}
