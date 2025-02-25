using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
using WingtipToys.Data; 
using WingtipToys.Models; 
namespace WingtipToys.Api.Controllers 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    public class ShoppingCartController : ControllerBase 
    { 
        private readonly ProductContext _context; 
        public ShoppingCartController(ProductContext context) 
        { 
            _context = context; 
        } 
        [HttpGet("{cartId}")] 
        public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems(string cartId) 
        { 
            try 
            { 
                var cartItems = await _context.ShoppingCartItems 
                    .Where(c => c.CartId == cartId) 
                    .Include(c => c.Product) 
                    .ToListAsync(); 
                if (cartItems == null || !cartItems.Any()) 
                { 
                    return NotFound(); 
                } 
                return Ok(cartItems); 
            } 
            catch (Exception ex) 
            { 
                Console.Error.WriteLine($"Error fetching cart items: {ex.Message}\n{ex.StackTrace}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        [HttpPost("{cartId}/add/{productId}")] 
        public async Task<IActionResult> AddToCart(string cartId, int productId) 
        { 
            try 
            { 
                var product = await _context.Products.FindAsync(productId); 
                if (product == null) 
                { 
                    return NotFound("Product not found"); 
                } 
                var cartItem = await _context.ShoppingCartItems 
                    .SingleOrDefaultAsync(c => c.CartId == cartId && c.ProductId == productId); 
                if (cartItem == null) 
                { 
                    cartItem = new CartItem 
                    { 
                        ItemId = Guid.NewGuid().ToString(), 
                        ProductId = productId, 
                        CartId = cartId, 
                        Product = product, 
                        Quantity = 1, 
                        DateCreated = DateTime.Now 
                    }; 
                    _context.ShoppingCartItems.Add(cartItem); 
                } 
                else 
                { 
                    cartItem.Quantity++; 
                } 
                await _context.SaveChangesAsync(); 
                return Ok(); 
            } 
            catch (Exception ex) 
            { 
                Console.Error.WriteLine($"Error adding to cart: {ex.Message}\n{ex.StackTrace}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        [HttpDelete("{cartId}/remove/{productId}")] 
        public async Task<IActionResult> RemoveFromCart(string cartId, int productId) 
        { 
            try 
            { 
                var cartItem = await _context.ShoppingCartItems 
                    .SingleOrDefaultAsync(c => c.CartId == cartId && c.ProductId == productId); 
                if (cartItem == null) 
                { 
                    return NotFound("Cart item not found"); 
                } 
                _context.ShoppingCartItems.Remove(cartItem); 
                await _context.SaveChangesAsync(); 
                return Ok(); 
            } 
            catch (Exception ex) 
            { 
                Console.Error.WriteLine($"Error removing from cart: {ex.Message}\n{ex.StackTrace}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        [HttpPut("{cartId}/update/{productId}")] 
        public async Task<IActionResult> UpdateCartItem(string cartId, int productId, [FromBody] int quantity) 
        { 
            try 
            { 
                if (quantity < 1) 
                { 
                    return BadRequest("Quantity must be at least 1"); 
                } 
                var cartItem = await _context.ShoppingCartItems 
                    .SingleOrDefaultAsync(c => c.CartId == cartId && c.ProductId == productId); 
                if (cartItem == null) 
                { 
                    return NotFound("Cart item not found"); 
                } 
                cartItem.Quantity = quantity; 
                await _context.SaveChangesAsync(); 
                return Ok(); 
            } 
            catch (Exception ex) 
            { 
                Console.Error.WriteLine($"Error updating cart item: {ex.Message}\n{ex.StackTrace}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        [HttpDelete("{cartId}/empty")] 
        public async Task<IActionResult> EmptyCart(string cartId) 
        { 
            try 
            { 
                var cartItems = _context.ShoppingCartItems.Where(c => c.CartId == cartId); 
                _context.ShoppingCartItems.RemoveRange(cartItems); 
                await _context.SaveChangesAsync(); 
                return Ok(); 
            } 
            catch (Exception ex) 
            { 
                Console.Error.WriteLine($"Error emptying cart: {ex.Message}\n{ex.StackTrace}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
    } 
} 