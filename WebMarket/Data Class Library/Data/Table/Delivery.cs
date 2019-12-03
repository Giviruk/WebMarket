using System;
using System.Collections.Generic;

namespace DataClassLibrary
{
    public partial class Delivery
    {
        public Delivery()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public DateTime? Datedelivery { get; set; }
        public string Pointofissue { get; set; }
        public string Addressdelivery { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
