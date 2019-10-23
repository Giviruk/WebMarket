using System;
using System.Collections.Generic;

namespace WebMarket
{
    public partial class Statuses
    {
        public Statuses()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
