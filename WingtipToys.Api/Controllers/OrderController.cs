using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 
using System; 
using System.Collections.Generic; 
using System.Threading.Tasks; 
using WingtipToys.Data; 
using WingtipToys.Models; 
namespace WingtipToys.Api.Controllers 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    public class OrderController : ControllerBase 
    { 
        private readonly ProductContext _context; 
        public OrderController(ProductContext context) 
        { 
            _context = context; 
        } 
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders() 
        { 
            try 
            { 
                var orders = await _context.Orders.ToListAsync(); 
                Console.WriteLine("Retrieved all orders successfully."); 
                return orders; 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error retrieving orders: {ex.Message}\n{ex.StackTrace}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        [HttpGet("{id}")] 
        public async Task<ActionResult<Order>> GetOrder(int id) 
        { 
            try 
            { 
                var order = await _context.Orders.FindAsync(id); 
                if (order == null) 
                { 
                    Console.WriteLine($"Order with ID {id} not found."); 
                    return NotFound(); 
                } 
                Console.WriteLine($"Retrieved order with ID {id} successfully."); 
                return order; 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error retrieving order with ID {id}: {ex.Message}\n{ex.StackTrace}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        [HttpPost] 
        public async Task<ActionResult<Order>> CreateOrder(Order order) 
        { 
            try 
            { 
                _context.Orders.Add(order); 
                await _context.SaveChangesAsync(); 
                Console.WriteLine($"Order created successfully with ID {order.OrderId}."); 
                return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order); 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error creating order: {ex.Message}\n{ex.StackTrace}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateOrder(int id, Order order) 
        { 
            if (id != order.OrderId) 
            { 
                Console.WriteLine($"Order ID mismatch: {id} != {order.OrderId}"); 
                return BadRequest(); 
            } 
            _context.Entry(order).State = EntityState.Modified; 
            try 
            { 
                await _context.SaveChangesAsync(); 
                Console.WriteLine($"Order with ID {id} updated successfully."); 
            } 
            catch (DbUpdateConcurrencyException ex) 
            { 
                if (!OrderExists(id)) 
                { 
                    Console.WriteLine($"Order with ID {id} not found for update."); 
                    return NotFound(); 
                } 
                else 
                { 
                    Console.WriteLine($"Concurrency error updating order with ID {id}: {ex.Message}\n{ex.StackTrace}"); 
                    throw; 
                } 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error updating order with ID {id}: {ex.Message}\n{ex.StackTrace}"); 
                return StatusCode(500, "Internal server error"); 
            } 
            return NoContent(); 
        } 
        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteOrder(int id) 
        { 
            try 
            { 
                var order = await _context.Orders.FindAsync(id); 
                if (order == null) 
                { 
                    Console.WriteLine($"Order with ID {id} not found for deletion."); 
                    return NotFound(); 
                } 
                _context.Orders.Remove(order); 
                await _context.SaveChangesAsync(); 
                Console.WriteLine($"Order with ID {id} deleted successfully."); 
                return NoContent(); 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error deleting order with ID {id}: {ex.Message}\n{ex.StackTrace}"); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        private bool OrderExists(int id) 
        { 
            return _context.Orders.Any(e => e.OrderId == id); 
        } 
    } 
} 