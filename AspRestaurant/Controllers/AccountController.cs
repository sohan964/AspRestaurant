using AspRestaurant.Models;
using AspRestaurant.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }



        [HttpPost("signup")] //createUser
        public async Task<IActionResult> CreateUser([FromBody] SignUp signUp)
        {
            var Result = await _accountRepository.SignUpAsync(signUp);
            if (Result.Succeeded)
            {
                return Ok(new { status=200 });
            }
            return Unauthorized();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignIn signIn)
        {
            var result = await _accountRepository.LoginAsync(signIn);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> CurrentUser()
        {
            var email = HttpContext.User?.Claims?.First()?.Value;
            if (email == null) return Unauthorized();
            var user = await _accountRepository.GetUserByEmailAsync(email);
            if (user == null) return NotFound();
            return Ok(user);


        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var email = HttpContext.User?.Claims?.First()?.Value;
            if (email == null) return NotFound();
            var isAdmin = await _accountRepository.isAdminAsync(email);
            if (!isAdmin)
            {
                return Unauthorized();
            }
            var allUsers = await _accountRepository.GetAllUserAsync();
            return Ok(allUsers);
        }

        [Authorize]
        [HttpPut("MakeAdmin/{email}")]
        public async Task<IActionResult> UpdateUserRole([FromRoute] string email)
        {
            var userEmail = HttpContext.User?.Claims?.First()?.Value;
            if(userEmail == null) return BadRequest();
            var isAdmin = await _accountRepository.isAdminAsync(userEmail);
            if (!isAdmin) return Unauthorized();

            var updateRole = await _accountRepository.UpdateUserRoleAsync(email);
            if(updateRole == null) return BadRequest();
            return Ok(updateRole);

        }

    }
}
