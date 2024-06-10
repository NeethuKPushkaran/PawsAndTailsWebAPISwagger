using Microsoft.EntityFrameworkCore;
using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ensure unique usernames

            modelBuilder.Entity<User>()
                 .HasIndex(u => u.UserName)
                 .IsUnique();

            // Configuring many-to-many relationship between Products and Categories

            modelBuilder.Entity<ProductCategory>()
                 .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<ProductCategory>()
                 .HasOne(pc => pc.Product)
                 .WithMany(p => p.ProductCategories)
                 .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategory>()
                 .HasOne(pc => pc.Category)
                 .WithMany(c => c.ProductCategories)
                 .HasForeignKey(pc => pc.CategoryId);

            // Configuring one-to-many relationship between User and Cart

            modelBuilder.Entity<Cart>()
                 .HasOne(c => c.User)
                 .WithMany(u => u.Carts)
                 .HasForeignKey(c => c.UserId);

            // Configuring one-to-many relationship between Cart and CartItem
            modelBuilder.Entity<CartItem>()
                 .HasOne(ci => ci.Cart)
                 .WithMany(c => c.CartItems)
                 .HasForeignKey(ci => ci.CartId);

            // Configuring one-to-many relationship between CartItem and Product
            modelBuilder.Entity<CartItem>()
                 .HasOne(ci => ci.Product)
                 .WithMany()
                 .HasForeignKey(ci => ci.ProductId);

            //Configuring one-to-many relationship between User and Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            //Configuring one-to-many relationship between Order and OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            //Configuring one-to-many relationship between OrderDetail and Product
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId);
        }
    }
}
