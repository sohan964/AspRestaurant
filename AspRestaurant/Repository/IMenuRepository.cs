using AspRestaurant.Data;
using AspRestaurant.Models;

namespace AspRestaurant.Repository
{
    public interface IMenuRepository
    {
        Task<List<Menu>> GetAllMenuAsync();
        Task<Menu> AddItemAsync(MenuModel menuModel);
        Task<Menu> GetMenuByIdAsync(int id);
        Task<Menu> UpdateItemAsync(int itemId, MenuModel menuModel);
        Task<bool> DeleteItemAsync(int itemId);
    }
}
