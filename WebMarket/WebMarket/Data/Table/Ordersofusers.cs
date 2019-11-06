using System;
using System.Collections.Generic;

namespace WebMarket
{
    public partial class Ordersofusers
    {
        public int Id { get; set; }
        public int? Userid { get; set; }
        public int? Orderid { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Users User { get; set; }
    }
}
