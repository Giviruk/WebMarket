using System;
using System.Collections.Generic;

namespace DataClassLibrary
{
    public partial class UserOrder
    {
        public int Id { get; set; }
        public int? Userid { get; set; }
        public int? Orderid { get; set; }

        public virtual Order Order { get; set; }
        public virtual User User { get; set; }
    }
}
