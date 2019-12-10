using System.Collections.Generic;

namespace DataClassLibrary
{
    public partial class OrderWithProductList
    {
        public Order Order { get; set; }

        public List<int> ProductIdList { get; set; }
    }
}
