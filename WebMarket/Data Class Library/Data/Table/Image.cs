using System.Collections.Generic;

namespace DataClassLibrary
{
    public partial class Image
    {
        public Image()
        {
            Product = new HashSet<Product>();
            Productimages = new HashSet<ProductImage>();
        }

        public int Id { get; set; }
        public string Imagepath { get; set; }
        public string Imagename { get; set; }

        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<ProductImage> Productimages { get; set; }
    }
}
