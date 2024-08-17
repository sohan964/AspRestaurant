using AspRestaurant.Data;
using AspRestaurant.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AspRestaurant.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly AspRestaurantContext _context;

        public AccountRepository(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, IConfiguration configuration,
            AspRestaurantContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        //createUser
        public async Task<IdentityResult> SignUpAsync(SignUp signUp)
        {
            var user = new ApplicationUser()
            {
                FullName = signUp.FullName,
                Email = signUp.Email,
                UserName = signUp.Email,
                Role = "user",
            };
            return await _userManager.CreateAsync(user, signUp.Password);
        }

        //Login
        public async Task<string> LoginAsync(SignIn signIn)
        {
            var result = await _signInManager.PasswordSignInAsync(signIn.Email, signIn.Password,false, false);
            if(!result.Succeeded)
            {
                return null;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, signIn.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            //new generate token
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //GetUserByEmail
        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            return user;
        }

        //check admin
        public async Task<bool> isAdminAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user?.Role =="admin") return true;
            return false;
            
        }

        //getAllUser
        public async Task<List<ApplicationUser>> GetAllUserAsync()
        {
            var users = await _userManager.Users.Select(user => new ApplicationUser
            {
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                Role = user.Role,
            }).ToListAsync();
            return users;
        }

        //Make admin
        public async Task<ApplicationUser> UpdateUserRoleAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new InvalidOperationException("user not found");
            user.Role = "admin";
            await _userManager.UpdateAsync(user);
            //_context.ApplicationUser.Update(user);
            return user;
        }



    }
}
