using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 
using WingtipToys.Data; 
using WingtipToys.Models; 
using System.Collections.Generic; 
using System.Threading.Tasks; 
using Microsoft.Extensions.Logging; 
namespace WingtipToys.Api.Controllers 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    public class ProductsController : ControllerBase 
    { 
        private readonly ProductContext _context; 
        private readonly ILogger<ProductsController> _logger; 
        public ProductsController(ProductContext context, ILogger<ProductsController> logger) 
        { 
            _context = context; 
            _logger = logger; 
        } 
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() 
        { 
            _logger.LogInformation("Fetching all products from the database."); 
            try 
            { 
                var products = await _context.Products.ToListAsync(); 
                _logger.LogInformation("Successfully fetched {Count} products.", products.Count); 
                return products; 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while fetching products."); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        [HttpGet("{id}")] 
        public async Task<ActionResult<Product>> GetProduct(int id) 
        { 
            _logger.LogInformation("Fetching product with ID {ProductId} from the database.", id); 
            try 
            { 
                var product = await _context.Products.FindAsync(id); 
                if (product == null) 
                { 
                    _logger.LogWarning("Product with ID {ProductId} not found.", id); 
                    return NotFound(); 
                } 
                _logger.LogInformation("Successfully fetched product with ID {ProductId}.", id); 
                return product; 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while fetching the product with ID {ProductId}.", id); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        // Additional CRUD methods (POST, PUT, DELETE) can be added here... 
    } 
} 