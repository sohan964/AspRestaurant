using AspRestaurant.Data;
using AspRestaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace AspRestaurant.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly AspRestaurantContext _context;

        public MenuRepository(AspRestaurantContext context)
        {
            _context = context;
        }

        //GetALLMenu
        public async Task<List<Menu>> GetAllMenuAsync()
        {
            
            var allMenu = await _context.Menus.Select(item => new Menu()
            {
                Id = item.Id,
                Name = item.Name,
                Recipe = item.Recipe,
                Image = item.Image,
                Price = item.Price,
                CategoryId = item.CategoryId,
                Category = item.Category,
            }).ToListAsync();

            return allMenu;
        }

        //GetItemById
        public async Task<Menu> GetMenuByIdAsync(int id)
        {
            var getItem = await _context.Menus.Where(x => x.Id == id).Select(x => new Menu() {
                Id = x.Id,
                Name = x.Name,
                Recipe = x.Recipe,
                Image = x.Image,
                Price = x.Price,
                CategoryId = x.CategoryId,
                Category = x.Category,

            }).FirstOrDefaultAsync();
            return getItem;
        }

        //add Item in the menu
        public async Task<Menu> AddItemAsync(MenuModel menuModel)
        {
            var newItem = new Menu()
            {
                Name = menuModel.Name,
                Recipe = menuModel.Recipe,
                Image = menuModel.Image,
                Price = menuModel.Price,
                CategoryId = menuModel.CategoryId,

            };
            _context.Menus.Add(newItem);
            await _context.SaveChangesAsync();
            return newItem;
        }

        //UpdateItem
        public async Task<Menu> UpdateItemAsync (int itemId, MenuModel menuModel)
        {
            var updateItem = new Menu()
            {
                Id= itemId,
                Name = menuModel.Name,
                Recipe = menuModel.Recipe,
                Image = menuModel.Image,
                Price = menuModel.Price,
                CategoryId = menuModel.CategoryId,

            };

            _context.Menus.Update(updateItem);
            await _context.SaveChangesAsync();
            return updateItem;
        }

        //public delete Item from menu
        public async Task<bool> DeleteItemAsync(int itemId)
        {
            var deleteItem = new Menu()
            {
                Id = itemId,
            };
            _context.Menus.Remove(deleteItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
