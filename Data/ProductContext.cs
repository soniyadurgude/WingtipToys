using Microsoft.EntityFrameworkCore; 
using WingtipToys.Models; 
using Microsoft.Extensions.Logging; 
namespace WingtipToys.Data 
{ 
    public class ProductContext : DbContext 
    { 
        public ProductContext(DbContextOptions<ProductContext> options) 
            : base(options) 
        { 
        } 
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Product> Products { get; set; } 
        public DbSet<CartItem> ShoppingCartItems { get; set; } 
        public DbSet<Order> Orders { get; set; } 
        public DbSet<OrderDetail> OrderDetails { get; set; } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        { 
            if (!optionsBuilder.IsConfigured) 
            { 
                optionsBuilder.UseSqlServer("YourConnectionStringHere"); // INPUT_REQUIRED {Database connection string} 
            } 
        } 
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { 
            // Configure entity filters, relationships, etc. 
            modelBuilder.Entity<Category>().ToTable("Categories"); 
            modelBuilder.Entity<Product>().ToTable("Products"); 
            modelBuilder.Entity<CartItem>().ToTable("ShoppingCartItems"); 
            modelBuilder.Entity<Order>().ToTable("Orders"); 
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetails"); 
            // Additional configurations can be added here 
        } 
    } 
} 