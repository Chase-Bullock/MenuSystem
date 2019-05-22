using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CathedralKitchen.NewModels
{
    public partial class CathedralKitchenContext : DbContext
    {
        public CathedralKitchenContext()
        {
        }

        public CathedralKitchenContext(DbContextOptions<CathedralKitchenContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Builder> Builder { get; set; }
        public virtual DbSet<BuilderCommunity> BuilderCommunity { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Community> Community { get; set; }
        public virtual DbSet<MenuItem> MenuItem { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderCode> OrderCode { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<OrderItemTopping> OrderItemTopping { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<ScheduleConfig> ScheduleConfig { get; set; }
        public virtual DbSet<SystemReference> SystemReference { get; set; }
        public virtual DbSet<Topping> Topping { get; set; }
        public virtual DbSet<ToppingSystemReference> ToppingSystemReference { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CathedralKitchen;Trusted_Connection=True;");
                //optionsBuilder.UseSqlServer("Server=CPT-SQL.cathedral.local;Database=CathedralKitchen;Persist Security Info=False;User Id=Chase.B;password=Finnava424;MultipleActiveResultSets=True;TrustServerCertificate=True;Connection Timeout=30;");
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Builder>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<BuilderCommunity>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Community>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Community)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_Community_Region");
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AddressLine1).HasMaxLength(255);

                entity.Property(e => e.AddressLine2).HasMaxLength(255);

                entity.Property(e => e.City).HasMaxLength(70);

                entity.Property(e => e.CompleteTime).HasColumnType("datetime");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CustomerEmail).HasMaxLength(100);

                entity.Property(e => e.CustomerFirstName)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.CustomerLastName)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.ZipCode).HasMaxLength(12);

                entity.HasOne(d => d.Community)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CommunityId)
                    .HasConstraintName("FK_Order_Community");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.OrderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_OrderStatus");
            });

            modelBuilder.Entity<OrderCode>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.MenuItem)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.MenuItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItem_MenuItem");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderItem_Order");
            });

            modelBuilder.Entity<OrderItemTopping>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.OrderItem)
                    .WithMany(p => p.OrderItemTopping)
                    .HasForeignKey(d => d.OrderItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItemTopping_OrderItem");

                entity.HasOne(d => d.Topping)
                    .WithMany(p => p.OrderItemTopping)
                    .HasForeignKey(d => d.ToppingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItemTopping_Topping");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Cell).HasMaxLength(20);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(160);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Home).HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.Work).HasMaxLength(20);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.RegionName).HasMaxLength(300);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ScheduleConfig>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Community)
                    .WithMany(p => p.ScheduleConfig)
                    .HasForeignKey(d => d.CommunityId)
                    .HasConstraintName("FK_ScheduleConfig_Community");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_ScheduleConfig_ScheduleConfig");
            });

            modelBuilder.Entity<SystemReference>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AltValue).HasMaxLength(100);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.MainValue)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Topping>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.ToppingName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ToppingSystemReference>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Topping)
                    .WithMany(p => p.ToppingSystemReference)
                    .HasForeignKey(d => d.ToppingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ToppingSystemReference_Topping");

                entity.HasOne(d => d.ToppingType)
                    .WithMany(p => p.ToppingSystemReference)
                    .HasForeignKey(d => d.ToppingTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ToppingSystemReference_SystemReference");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Hash).HasMaxLength(255);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });
        }
    }
}
