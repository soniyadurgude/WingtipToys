using Microsoft.AspNetCore.Mvc; 
using WingtipToys.Models; 
using System; 
using System.Linq; 
using Microsoft.Extensions.Logging; 
namespace WingtipToys.Controllers 
{ 
    [ApiController] 
    [Route("api/[controller]")] 
    public class ShoppingCartController : ControllerBase 
    { 
        private readonly ProductContext _context; 
        private readonly ILogger<ShoppingCartController> _logger; 
        public ShoppingCartController(ProductContext context, ILogger<ShoppingCartController> logger) 
        { 
            _context = context; 
            _logger = logger; 
        } 
        [HttpPost("AddToCart")] 
        public IActionResult AddToCart(int productId) 
        { 
            try 
            { 
                var cartId = GetCartId(); 
                var cartItem = _context.ShoppingCartItems.SingleOrDefault( 
                    c => c.CartId == cartId && c.ProductId == productId); 
                if (cartItem == null) 
                { 
                    cartItem = new CartItem 
                    { 
                        ItemId = Guid.NewGuid().ToString(), 
                        ProductId = productId, 
                        CartId = cartId, 
                        Product = _context.Products.SingleOrDefault(p => p.ProductID == productId), 
                        Quantity = 1, 
                        DateCreated = DateTime.Now 
                    }; 
                    _context.ShoppingCartItems.Add(cartItem); 
                } 
                else 
                { 
                    cartItem.Quantity++; 
                } 
                _context.SaveChanges(); 
                _logger.LogInformation($"Product with ID {productId} added to cart successfully."); 
                return Ok(); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, $"ERROR: Unable to add product to cart. {ex.Message}"); 
                return BadRequest(new { message = ex.Message }); 
            } 
        } 
        [HttpGet("GetCartItems")] 
        public IActionResult GetCartItems() 
        { 
            try 
            { 
                var cartId = GetCartId(); 
                var cartItems = _context.ShoppingCartItems.Where(c => c.CartId == cartId).ToList(); 
                _logger.LogInformation($"Retrieved {cartItems.Count} items from shopping cart."); 
                return Ok(cartItems); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, $"ERROR: Unable to retrieve shopping cart items. {ex.Message}"); 
                return BadRequest(new { message = ex.Message }); 
            } 
        } 
        private string GetCartId() 
        { 
            // Placeholder for session-based cart ID retrieval 
            return "some-cart-id"; // Replace with actual session management logic 
        } 
    } 
} 