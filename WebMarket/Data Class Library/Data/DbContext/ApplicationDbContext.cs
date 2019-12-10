using Microsoft.EntityFrameworkCore;

namespace DataClassLibrary.DbContext
{
    public abstract class AbstractDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Delivery> Deliveries { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<UserOrder> UserOrders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }

        //public  AbstractDbContext(DbContextOptions<d6h4jeg5tcb9d8Context> options)
        //    :base(options)
        //{

        //}
    }
}
