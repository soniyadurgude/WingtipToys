using System.Data.Entity; 
using System.Diagnostics; 
namespace WingtipToys.Models 
{ 
    public class ProductContext : DbContext 
    { 
        public ProductContext() 
          : base("WingtipToys") 
        { 
            Database.Log = s => Debug.WriteLine(s); // Log database operations for debugging 
        } 
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Product> Products { get; set; } 
        public DbSet<CartItem> ShoppingCartItems { get; set; } 
        public DbSet<Order> Orders { get; set; } 
        public DbSet<OrderDetail> OrderDetails { get; set; } 
        protected override void OnModelCreating(DbModelBuilder modelBuilder) 
        { 
            // Configure model with Fluent API if needed 
            // Example: modelBuilder.Entity<Product>().Property(p => p.ProductName).IsRequired(); 
        } 
    } 
}