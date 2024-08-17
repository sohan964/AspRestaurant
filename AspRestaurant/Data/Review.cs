using System.ComponentModel.DataAnnotations;

namespace AspRestaurant.Data
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int Rating { get; set; }
    }
}
