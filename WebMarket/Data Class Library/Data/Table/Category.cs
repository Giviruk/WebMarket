﻿using System.Collections.Generic;

namespace DataClassLibrary
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Characteristics { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
