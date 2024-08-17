using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspRestaurant.Data
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Recipe { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }

        //Foreign Key property
        public int CategoryId { get; set; }
        // Navigation property
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        //Navigation property
        public ICollection<Card> Cards { get; set; }



    }
}
