using System.Collections.Generic;
using Newtonsoft.Json;

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

        [JsonIgnore]
        public virtual ICollection<Product> Product { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProductImage> Productimages { get; set; }
    }
}
