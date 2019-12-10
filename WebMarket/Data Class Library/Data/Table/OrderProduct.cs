
namespace DataClassLibrary
{
    public partial class OrderProduct
    {
        public int Id { get; set; }
        public int? Productid { get; set; }
        public int? Orderid { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
