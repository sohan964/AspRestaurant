using AspRestaurant.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspRestaurant.Data
{
    public class AmountDetails
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }

        public string TransactionId { get; set; }

        public float Amount { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
