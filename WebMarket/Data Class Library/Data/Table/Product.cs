using System;
using System.Collections.Generic;

namespace DataClassLibrary
{
    public partial class Product
    {
        public Product()
        {
            ProductImages = new HashSet<ProductImage>();
            Review = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public int? Category { get; set; }
        public string Producer { get; set; }
        public int? Mainpictureurl { get; set; }
        public string Characteristics { get; set; }

        public float? ProductRating { get; set; }

        public virtual Category CategoryNavigation { get; set; }
        public virtual Image MainpictureurlNavigation { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}
