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
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

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
            base.OnModelCreating(modelBuilder);
        }
    }
}
