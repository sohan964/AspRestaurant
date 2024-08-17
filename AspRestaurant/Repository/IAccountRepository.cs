using AspRestaurant.Models;
using Microsoft.AspNetCore.Identity;

namespace AspRestaurant.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUp signUp);
        Task<string> LoginAsync(SignIn signIn);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<bool> isAdminAsync(string email);
        Task<List<ApplicationUser>> GetAllUserAsync();
        Task<ApplicationUser> UpdateUserRoleAsync(string email);
    }
}
