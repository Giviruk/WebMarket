using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataClassLibrary
{
    public partial class Users
    {
        public Users()
        {
            Orders = new HashSet<Orders>();
            Ordersofusers = new HashSet<Ordersofusers>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public int? City { get; set; }
        public string Login { get; set; }
<<<<<<< HEAD
||||||| merged common ancestors
        [JsonIgnore]
=======
        public string Token { get; set; }
>>>>>>> abc2bd293c2a6da639fa121dfe38f57e964a17ae
        public string Pass { get; set; }
        public string Addres { get; set; }

        public string Token { get; set; }

        
        public virtual Cities CityNavigation { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Ordersofusers> Ordersofusers { get; set; }
    }
}
