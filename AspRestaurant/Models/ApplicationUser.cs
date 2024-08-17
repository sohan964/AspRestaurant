using AspRestaurant.Data;
using Microsoft.AspNetCore.Identity;

namespace AspRestaurant.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Role { get; set; }

        //Navigation Property
        public ICollection<Card> Cards { get; set; }
        public ICollection<AmountDetails> AmountDetails { get; set; }

    }
}
