using AspRestaurant.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspRestaurant.Data
{
    public class AspRestaurantContext : IdentityDbContext<ApplicationUser>
    {
        public AspRestaurantContext(DbContextOptions<AspRestaurantContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<AmountDetails> AmountDetails { get; set; }
        
    }
}
