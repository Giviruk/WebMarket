using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebMarket
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Deliveries> Deliveries { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Ordersofusers> Ordersofusers { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Productimages> Productimages { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=root");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("adminpack");

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Cities>(entity =>
            {
                entity.ToTable("cities");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Deliveries>(entity =>
            {
                entity.ToTable("deliveries");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Addressdelivery)
                    .HasColumnName("addressdelivery")
                    .HasMaxLength(255);

                entity.Property(e => e.Datedelivery)
                    .HasColumnName("datedelivery")
                    .HasColumnType("date");

                entity.Property(e => e.Pointofissue)
                    .HasColumnName("pointofissue")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Images>(entity =>
            {
                entity.ToTable("images");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Imagename)
                    .HasColumnName("imagename")
                    .HasMaxLength(255);

                entity.Property(e => e.Imagepath)
                    .HasColumnName("imagepath")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datecreated).HasColumnName("datecreated");

                entity.Property(e => e.Delivery).HasColumnName("delivery");

                entity.Property(e => e.Owner).HasColumnName("owner");

                entity.Property(e => e.Product).HasColumnName("product");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.DeliveryNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Delivery)
                    .HasConstraintName("orders_delivery_fkey");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Owner)
                    .HasConstraintName("orders_owner_fkey");

                entity.HasOne(d => d.ProductNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Product)
                    .HasConstraintName("orders_product_fkey");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("orders_status_fkey");
            });

            modelBuilder.Entity<Ordersofusers>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ordersofusers");

                entity.Property(e => e.Orderid).HasColumnName("orderid");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Ordersofusers)
                    .HasForeignKey(d => d.Orderid)
                    .HasConstraintName("ordersofusers_orderid_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ordersofusers)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("ordersofusers_userid_fkey");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.Characteristics)
                    .HasColumnName("characteristics")
                    .HasMaxLength(1000);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(1000);

                entity.Property(e => e.Mainpictureurl).HasColumnName("mainpictureurl");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Producer)
                    .HasColumnName("producer")
                    .HasMaxLength(255);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("product_category_fkey");

                entity.HasOne(d => d.MainpictureurlNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.Mainpictureurl)
                    .HasConstraintName("product_mainpictureurl_fkey");
            });

            modelBuilder.Entity<Productimages>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("productimages");

                entity.Property(e => e.Imageid).HasColumnName("imageid");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Productimages)
                    .HasForeignKey(d => d.Imageid)
                    .HasConstraintName("productimages_imageid_fkey");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Productimages)
                    .HasForeignKey(d => d.Productid)
                    .HasConstraintName("productimages_productid_fkey");
            });

            modelBuilder.Entity<Statuses>(entity =>
            {
                entity.ToTable("statuses");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.Firstname)
                    .HasColumnName("firstname")
                    .HasMaxLength(255);

                entity.Property(e => e.Lastname)
                    .HasColumnName("lastname")
                    .HasMaxLength(255);

                entity.Property(e => e.Middlename)
                    .HasColumnName("middlename")
                    .HasMaxLength(255);

                entity.HasOne(d => d.CityNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.City)
                    .HasConstraintName("users_city_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
