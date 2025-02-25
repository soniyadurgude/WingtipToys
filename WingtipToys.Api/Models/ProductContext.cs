using Microsoft.EntityFrameworkCore; 
using WingtipToys.Models; 
namespace WingtipToys.Api.Models 
{ 
    public class ProductContext : DbContext 
    { 
        public ProductContext(DbContextOptions<ProductContext> options) 
            : base(options) 
        { 
            // Log the creation of the ProductContext instance 
            Console.WriteLine("ProductContext instance created."); 
        } 
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Product> Products { get; set; } 
        public DbSet<CartItem> ShoppingCartItems { get; set; } 
        public DbSet<Order> Orders { get; set; } 
        public DbSet<OrderDetail> OrderDetails { get; set; } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        { 
            try 
            { 
                if (!optionsBuilder.IsConfigured) 
                { 
                    // Log the configuration of the database context 
                    Console.WriteLine("Configuring the database context."); 
                    optionsBuilder.UseSqlServer("YourConnectionStringHere"); // INPUT_REQUIRED {Database connection string} 
                } 
            } 
            catch (Exception ex) 
            { 
                // Log the error with full stack trace 
                Console.WriteLine($"Error configuring the database context: {ex.Message}"); 
                Console.WriteLine(ex.StackTrace); 
            } 
        } 
    } 
} 