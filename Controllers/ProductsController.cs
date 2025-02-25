using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 
using WingtipToys.Data; 
using WingtipToys.Models; 
using System.Collections.Generic; 
using System.Threading.Tasks; 
using Microsoft.Extensions.Logging; 
namespace WingtipToys.Controllers 
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
        [HttpPost] 
        public async Task<ActionResult<Product>> CreateProduct(Product product) 
        { 
            _logger.LogInformation("Creating a new product."); 
            try 
            { 
                _context.Products.Add(product); 
                await _context.SaveChangesAsync(); 
                _logger.LogInformation("Successfully created a new product with ID {ProductId}.", product.ProductID); 
                return CreatedAtAction(nameof(GetProduct), new { id = product.ProductID }, product); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while creating a new product."); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateProduct(int id, Product product) 
        { 
            if (id != product.ProductID) 
            { 
                _logger.LogWarning("Product ID mismatch: {ProductId} does not match the route ID {RouteId}.", product.ProductID, id); 
                return BadRequest(); 
            } 
            _logger.LogInformation("Updating product with ID {ProductId}.", id); 
            try 
            { 
                _context.Entry(product).State = EntityState.Modified; 
                await _context.SaveChangesAsync(); 
                _logger.LogInformation("Successfully updated product with ID {ProductId}.", id); 
                return NoContent(); 
            } 
            catch (DbUpdateConcurrencyException ex) 
            { 
                if (!ProductExists(id)) 
                { 
                    _logger.LogWarning("Product with ID {ProductId} not found for update.", id); 
                    return NotFound(); 
                } 
                else 
                { 
                    _logger.LogError(ex, "A concurrency error occurred while updating the product with ID {ProductId}.", id); 
                    throw; 
                } 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while updating the product with ID {ProductId}.", id); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteProduct(int id) 
        { 
            _logger.LogInformation("Deleting product with ID {ProductId}.", id); 
            try 
            { 
                var product = await _context.Products.FindAsync(id); 
                if (product == null) 
                { 
                    _logger.LogWarning("Product with ID {ProductId} not found for deletion.", id); 
                    return NotFound(); 
                } 
                _context.Products.Remove(product); 
                await _context.SaveChangesAsync(); 
                _logger.LogInformation("Successfully deleted product with ID {ProductId}.", id); 
                return NoContent(); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while deleting the product with ID {ProductId}.", id); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        private bool ProductExists(int id) 
        { 
            return _context.Products.Any(e => e.ProductID == id); 
        } 
    } 
} using Microsoft.AspNetCore.Mvc; 
using WingtipToys.Models; 
using System.Linq; 
using System.Diagnostics; 
namespace WingtipToys.Controllers 
{ 
    [ApiController] 
    [Route("api/[controller]")] 
    public class ProductsController : ControllerBase 
    { 
        private readonly ProductContext _context; 
        public ProductsController(ProductContext context) 
        { 
            _context = context; 
        } 
        [HttpGet] 
        public IActionResult GetProducts(int? categoryId) 
        { 
            try 
            { 
                var query = _context.Products.AsQueryable(); 
                if (categoryId.HasValue && categoryId > 0) 
                { 
                    query = query.Where(p => p.CategoryID == categoryId); 
                    Debug.WriteLine($"Products filtered by category ID {categoryId}."); 
                } 
                else 
                { 
                    Debug.WriteLine("No category ID provided, retrieving all products."); 
                } 
                var products = query.ToList(); 
                Debug.WriteLine($"Retrieved {products.Count} products."); 
                return Ok(products); 
            } 
            catch (Exception ex) 
            { 
                Debug.WriteLine($"ERROR: Unable to retrieve products. {ex.Message} \n {ex.StackTrace}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        [HttpGet("{productId}")] 
        public IActionResult GetProduct(int productId) 
        { 
            try 
            { 
                var product = _context.Products.SingleOrDefault(p => p.ProductID == productId); 
                if (product == null) 
                { 
                    Debug.WriteLine($"Product with ID {productId} not found."); 
                    return NotFound(); 
                } 
                Debug.WriteLine($"Product with ID {productId} retrieved successfully."); 
                return Ok(product); 
            } 
            catch (Exception ex) 
            { 
                Debug.WriteLine($"ERROR: Unable to retrieve product. {ex.Message} \n {ex.StackTrace}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
    } 
} 