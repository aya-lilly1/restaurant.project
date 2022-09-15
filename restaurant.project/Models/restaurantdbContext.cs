using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace restaurant.project.Models
{
    public partial class restaurantdbContext : DbContext
    {
        public bool IgnoreFilter { get; set; }
        public bool IgnoreFilterCust { get; set; }
        public bool IgnoreFilterOrder { get; set; }
        public bool IgnoreFilterMenu { get; set; }
        public bool IgnoreFilterRestaurnt { get; set; }

        public restaurantdbContext()
        {
        }

        public restaurantdbContext(DbContextOptions<restaurantdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Restaurantmenu> Restaurantmenus { get; set; }
        public virtual DbSet<CsvView> CsvViews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseMySQL("Server=localhost;port=3306;user=root;password=Manager@123456;database=restaurantdb;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CsvView>(entity =>
            {
                entity.ToTable("csvview");
                entity.HasNoKey();


                entity.Property(e => e.ResturentName)
                    .HasColumnType("varchar ");

                entity.Property(e => e.NumberOfOrderCoustomer)
                   .HasColumnType("int ");

                entity.Property(e => e.ProFitInUsd)
                  .HasColumnType("float ");

                entity.Property(e => e.ProFitInNis)
                  .HasColumnType("float ");

                entity.Property(e => e.TheBestSellingMeal)
                 .HasColumnType("varchar ");

                entity.Property(e => e.MostPurchasedCustomer)
                .HasColumnType("varchar ");
            });
                modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int unsigned")
                    .HasColumnName("id");

              

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("firstName")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("lastName");

                entity.Property(e => e.CreatedDate)
                     .HasColumnName("createdDate")
                     .HasColumnType("timestamp")
                     .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedDate)
                      .HasColumnName("updateDate")
                      .HasColumnType("timestamp")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Archived).HasColumnType("tinyint(3)");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CustomerId, "order-cust_idx");

                entity.HasIndex(e => e.MealId, "order-meal_idx");

                entity.HasIndex(e => e.RestaurantId, "order-res_idx");

                entity.Property(e => e.Id).HasColumnType("int unsigned");

                

                entity.Property(e => e.CustomerId)
                    .HasColumnType("int unsigned")
                    .HasColumnName("customerId");

                entity.Property(e => e.MealId)
                    .HasColumnType("int unsigned")
                    .HasColumnName("mealId");

                entity.Property(e => e.Quatity)
                    .HasColumnType("int unsigned")
                    .HasColumnName("quatity");

                entity.Property(e => e.RestaurantId)
                    .HasColumnType("int unsigned")
                    .HasColumnName("restaurantId");


                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedDate)
                      .HasColumnName("updateDate")
                      .HasColumnType("timestamp")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Archived).HasColumnType("tinyint(3)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order-cust");

                entity.HasOne(d => d.Meal)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order-meal");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order-res");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("restaurant");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "phone_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int unsigned");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("phone");

                entity.Property(e => e.RestaurantName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("restaurantName")
                    .HasDefaultValueSql("''");


                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedDate)
                      .HasColumnName("updateDate")
                      .HasColumnType("timestamp")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Archived).HasColumnType("tinyint(3)");
            });

            modelBuilder.Entity<Restaurantmenu>(entity =>
            {
                entity.ToTable("restaurantmenu");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ResurantId, "menu-res_idx");

                entity.Property(e => e.Id).HasColumnType("int unsigned");

                entity.Property(e => e.Archived).HasColumnName("archived");

                entity.Property(e => e.MaelName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("maelName")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Quatity)
                    .HasColumnType("int unsigned")
                    .HasColumnName("quatity");
                entity.Property(e => e.PriceUsd)
                    .HasColumnType("int unsigned")
                    .HasColumnName("priceUsd");

                entity.Property(e => e.PriceNis)
                    .HasColumnType("int unsigned")
                    .HasColumnName("priceNis");

                entity.Property(e => e.ResurantId)
                    .HasColumnType("int unsigned")
                    .HasColumnName("resurantId");

                entity.HasOne(d => d.Resurant)
                    .WithMany(p => p.Restaurantmenus)
                    .HasForeignKey(d => d.ResurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("menu-res");


                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedDate)
                      .HasColumnName("updateDate")
                      .HasColumnType("timestamp")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Archived).HasColumnType("tinyint(3)");
            });

            OnModelCreatingPartial(modelBuilder);
            modelBuilder.Entity<Customer>().HasQueryFilter(a => !a.Archived|| IgnoreFilter||IgnoreFilterCust);
            modelBuilder.Entity<Order>().HasQueryFilter(a => !a.Archived|| IgnoreFilter||IgnoreFilterOrder);
            modelBuilder.Entity<Restaurant>().HasQueryFilter(a => !a.Archived|| IgnoreFilter||IgnoreFilterRestaurnt);
            modelBuilder.Entity<Restaurantmenu>().HasQueryFilter(a => !a.Archived||IgnoreFilter||IgnoreFilterMenu);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
