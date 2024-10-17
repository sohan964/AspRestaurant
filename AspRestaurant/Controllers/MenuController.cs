using AspRestaurant.Models;
using AspRestaurant.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IAccountRepository _accountRepository;

        public MenuController(IMenuRepository menuRepository,IAccountRepository accountRepository)
        {
            _menuRepository = menuRepository;
            _accountRepository = accountRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllMenu()
        {
            var allMenu = await _menuRepository.GetAllMenuAsync();
            return Ok(allMenu);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuById([FromRoute] int id)
        {
            var item = await _menuRepository.GetMenuByIdAsync(id);
            return Ok(item);
        }

        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> AddItem([FromBody]MenuModel menuModel)
        {
            //check admin or not
            var email = HttpContext.User?.Claims?.First()?.Value;
            if (email == null) return Unauthorized();
            var isAdmin = await _accountRepository.isAdminAsync(email);
            if (!isAdmin)
            {
                return Unauthorized();
            }

            if (menuModel == null) return BadRequest();
            var newItem = await _menuRepository.AddItemAsync(menuModel);
            Console.WriteLine(newItem);
            //return Ok(newItem);
            return Ok(new { InsertedId= newItem.Id });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateItem([FromRoute]int id,[FromBody]MenuModel menuModel)
        {
            //check admin or not
            var email = HttpContext.User?.Claims?.First()?.Value;
            if (email == null) return Unauthorized();
            var isAdmin = await _accountRepository.isAdminAsync(email);
            if (!isAdmin)
            {
                return Unauthorized();
            }

            var updateItem = await _menuRepository.UpdateItemAsync(id,menuModel);
            return Ok(updateItem);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteItem([FromRoute]int id)
        {
            //check admin or not
            var email = HttpContext.User?.Claims?.First()?.Value;
            if (email == null) return Unauthorized();
            var isAdmin = await _accountRepository.isAdminAsync(email);
            if (!isAdmin)
            {
                return Unauthorized();
            }
            var deleteItem = await _menuRepository.DeleteItemAsync(id);
            return Ok(new {deletedCount=deleteItem});
        }
    }
}
