namespace AspRestaurant.Models
{
    public class MenuModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Recipe { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public float Price { get; set; }

        public int CategoryId { get; set; }
    }
}
