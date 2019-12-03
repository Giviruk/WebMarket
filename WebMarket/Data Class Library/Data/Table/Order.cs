using System;
using System.Collections.Generic;

namespace DataClassLibrary
{
    public partial class Order
    {
        public Order()
        {
            Ordersofusers = new HashSet<UserOrder>();
        }

        public int Id { get; set; }
        public int? Owner { get; set; }
        public int? Status { get; set; }
        public int? Datecreated { get; set; }
        public int? Delivery { get; set; }

        public virtual Delivery DeliveryNavigation { get; set; }
        public virtual User OwnerNavigation { get; set; }
        public virtual Status StatusNavigation { get; set; }
        public virtual ICollection<UserOrder> Ordersofusers { get; set; }
    }
}
