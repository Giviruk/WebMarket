using DataClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Class_Library
{
    public partial class OrderWithProductList
    {
        public Order Order { get; set; }

        public List<int> ProductIdList { get; set; }
    }
}
