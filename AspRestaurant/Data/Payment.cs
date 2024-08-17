using AspRestaurant.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspRestaurant.Data
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }

        public string TransactionId { get; set; }

        public int MenuId { get; set; }

        [ForeignKey("MenuId")]
        public Menu Menu { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
