using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Store.Data
{
    public partial class PizzaPalaceContext : DbContext
    {
        public PizzaPalaceContext()
        {
        }

        public PizzaPalaceContext(DbContextOptions<PizzaPalaceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<OrderHasPizza> OrderHasPizza { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:valentin-quinones-1806.database.windows.net,1433;Initial Catalog=PizzaPalace;Persist Security Info=False;User ID=NinjaUltraStar;Password=Number39@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.IdLocation);

                entity.Property(e => e.IdLocation).HasColumnName("idLocation");

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderHasPizza>(entity =>
            {
                entity.HasKey(e => new { e.OrderIdOrder, e.OrderLocationIdLocation, e.OrderUserIdUser, e.OrderUserLocationIdLocation, e.PizzaIdPizza, e.IdOrderHasPizza });

                entity.ToTable("Order_has_Pizza");

                entity.HasIndex(e => e.PizzaIdPizza)
                    .HasName("fk_Order_has_Pizza_Pizza1_idx");

                entity.HasIndex(e => new { e.OrderIdOrder, e.OrderLocationIdLocation, e.OrderUserIdUser, e.OrderUserLocationIdLocation })
                    .HasName("fk_Order_has_Pizza_Order1_idx");

                entity.Property(e => e.OrderIdOrder).HasColumnName("Order_idOrder");

                entity.Property(e => e.OrderLocationIdLocation).HasColumnName("Order_Location_idLocation");

                entity.Property(e => e.OrderUserIdUser).HasColumnName("Order_User_idUser");

                entity.Property(e => e.OrderUserLocationIdLocation).HasColumnName("Order_User_Location_idLocation");

                entity.Property(e => e.PizzaIdPizza).HasColumnName("Pizza_idPizza");

                entity.Property(e => e.IdOrderHasPizza)
                    .HasColumnName("id_Order_has_Pizza")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.PizzaIdPizzaNavigation)
                    .WithMany(p => p.OrderHasPizza)
                    .HasForeignKey(d => d.PizzaIdPizza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Order_has_Pizza_Pizza1");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderHasPizza)
                    .HasForeignKey(d => new { d.OrderIdOrder, d.OrderLocationIdLocation, d.OrderUserIdUser, d.OrderUserLocationIdLocation })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Order_has_Pizza_Order1");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => new { e.IdOrder, e.LocationIdLocation, e.UserIdUser, e.UserLocationIdLocation });

                entity.HasIndex(e => e.LocationIdLocation)
                    .HasName("fk_Order_Location1_idx");

                entity.HasIndex(e => new { e.UserIdUser, e.UserLocationIdLocation })
                    .HasName("fk_Order_User1_idx");

                entity.Property(e => e.IdOrder)
                    .HasColumnName("idOrder")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LocationIdLocation).HasColumnName("Location_idLocation");

                entity.Property(e => e.UserIdUser).HasColumnName("User_idUser");

                entity.Property(e => e.UserLocationIdLocation).HasColumnName("User_Location_idLocation");

                entity.HasOne(d => d.LocationIdLocationNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.LocationIdLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Order_Location1");
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.HasKey(e => e.IdPizza);

                entity.Property(e => e.IdPizza).HasColumnName("idPizza");

                entity.Property(e => e.PizzaName)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PizzaPrice).HasColumnType("decimal(9, 2)");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.HasIndex(e => e.LocationIdLocation)
                    .HasName("fk_User_Location_idx");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LocationIdLocation).HasColumnName("Location_idLocation");

                entity.HasOne(d => d.LocationIdLocationNavigation)
                    .WithMany(p => p.UserInfo)
                    .HasForeignKey(d => d.LocationIdLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_User_Location");
            });
        }
    }
}
