using System;
using System.Collections.Generic;

namespace DataClassLibrary
{
    public partial class Images
    {
        public Images()
        {
            Product = new HashSet<Product>();
            Productimages = new HashSet<Productimages>();
        }

        public int Id { get; set; }
        public string Imagepath { get; set; }
        public string Imagename { get; set; }

        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<Productimages> Productimages { get; set; }
    }
}
