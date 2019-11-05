using System;
using System.Collections.Generic;

namespace WebMarket
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Orders>();
            Productimages = new HashSet<Productimages>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public int? Category { get; set; }
        public string Producer { get; set; }
        public int? Mainpictureurl { get; set; }
        public string Characteristics { get; set; }

        public virtual Categories CategoryNavigation { get; set; }
        public virtual Images MainpictureurlNavigation { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Productimages> Productimages { get; set; }
    }
}
