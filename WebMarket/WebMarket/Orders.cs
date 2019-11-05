using System;
using System.Collections.Generic;

namespace WebMarket
{
    public partial class Orders
    {
        public Orders()
        {
            Ordersofusers = new HashSet<Ordersofusers>();
        }

        public int Id { get; set; }
        public int? Product { get; set; }
        public int? Owner { get; set; }
        public int? Status { get; set; }
        public int? Datecreated { get; set; }
        public int? Delivery { get; set; }

        public virtual Deliveries DeliveryNavigation { get; set; }
        public virtual Users OwnerNavigation { get; set; }
        public virtual Product ProductNavigation { get; set; }
        public virtual Statuses StatusNavigation { get; set; }
        public virtual ICollection<Ordersofusers> Ordersofusers { get; set; }
    }
}
