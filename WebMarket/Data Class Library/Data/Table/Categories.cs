﻿using System;
using System.Collections.Generic;

namespace DataClassLibrary
{
    public partial class Categories
    {
        public Categories()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Characteristics { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}