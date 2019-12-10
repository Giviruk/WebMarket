using System.Collections.Generic;

namespace DataClassLibrary
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            Ordersofusers = new HashSet<UserOrder>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public int? City { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public string Address { get; set; }
        public string Token { get; set; }

        public virtual City CityNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<UserOrder> Ordersofusers { get; set; }
    }
}
