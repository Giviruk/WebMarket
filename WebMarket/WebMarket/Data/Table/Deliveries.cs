using System;
using System.Collections.Generic;

namespace WebMarket
{
    public partial class Deliveries
    {
        public Deliveries()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public DateTime? Datedelivery { get; set; }
        public string Pointofissue { get; set; }
        public string Addressdelivery { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
