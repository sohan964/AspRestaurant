using AspRestaurant.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspRestaurant.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public string TransactionId { get; set; }

        public List<int> ItemsId { get; set; } = new List<int>();

        public float TotalAmount { get; set; }
        

    }
}
