﻿namespace AspRestaurant.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //// Navigation property
        public ICollection<Menu> Menus { get; set; }
    }
}
