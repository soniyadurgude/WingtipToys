using System; 
using System.Collections.Generic; 
using System.Linq; 
using Microsoft.EntityFrameworkCore; 
using WingtipToys.Models; 
using Microsoft.Extensions.Logging; 
using Microsoft.AspNetCore.Http; 
namespace WingtipToys.Logic 
{ 
    public class ShoppingCartActions : IDisposable 
    { 
        public string ShoppingCartId { get; set; } 
        private readonly ProductContext _db; 
        private readonly ILogger<ShoppingCartActions> _logger; 
        private readonly IHttpContextAccessor _httpContextAccessor; 
        public const string CartSessionKey = "CartId"; 
        public ShoppingCartActions(ProductContext context, ILogger<ShoppingCartActions> logger, IHttpContextAccessor httpContextAccessor) 
        { 
            _db = context; 
            _logger = logger; 
            _httpContextAccessor = httpContextAccessor; 
        } 
        public void AddToCart(int id) 
        { 
            try 
            { 
                ShoppingCartId = GetCartId(); 
                var cartItem = _db.ShoppingCartItems.SingleOrDefault( 
                    c => c.CartId == ShoppingCartId && c.ProductId == id); 
                if (cartItem == null) 
                { 
                    cartItem = new CartItem 
                    { 
                        ItemId = Guid.NewGuid().ToString(), 
                        ProductId = id, 
                        CartId = ShoppingCartId, 
                        Product = _db.Products.SingleOrDefault(p => p.ProductID == id), 
                        Quantity = 1, 
                        DateCreated = DateTime.Now 
                    }; 
                    _db.ShoppingCartItems.Add(cartItem); 
                } 
                else 
                { 
                    cartItem.Quantity++; 
                } 
                _db.SaveChanges(); 
                _logger.LogInformation($"Product with ID {id} added to cart successfully."); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, $"ERROR: Unable to add product to cart. {ex.Message}"); 
                throw new Exception("ERROR: Unable to add product to cart.", ex); 
            } 
        } 
        public void Dispose() 
        { 
            _db?.Dispose(); 
        } 
        public string GetCartId() 
        { 
            var session = _httpContextAccessor.HttpContext.Session; 
            if (session.GetString(CartSessionKey) == null) 
            { 
                if (!string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.User.Identity.Name)) 
                { 
                    session.SetString(CartSessionKey, _httpContextAccessor.HttpContext.User.Identity.Name); 
                } 
                else 
                { 
                    Guid tempCartId = Guid.NewGuid(); 
                    session.SetString(CartSessionKey, tempCartId.ToString()); 
                } 
            } 
            return session.GetString(CartSessionKey); 
        } 
        public List<CartItem> GetCartItems() 
        { 
            ShoppingCartId = GetCartId(); 
            return _db.ShoppingCartItems.Where(c => c.CartId == ShoppingCartId).ToList(); 
        } 
        public decimal GetTotal() 
        { 
            ShoppingCartId = GetCartId(); 
            decimal? total = (from cartItems in _db.ShoppingCartItems 
                              where cartItems.CartId == ShoppingCartId 
                              select (int?)cartItems.Quantity * cartItems.Product.UnitPrice).Sum(); 
            return total ?? decimal.Zero; 
        } 
        public ShoppingCartActions GetCart() 
        { 
            return new ShoppingCartActions(_db, _logger, _httpContextAccessor) { ShoppingCartId = GetCartId() }; 
        } 
        public void UpdateShoppingCartDatabase(string cartId, ShoppingCartUpdates[] CartItemUpdates) 
        { 
            try 
            { 
                int CartItemCount = CartItemUpdates.Count(); 
                List<CartItem> myCart = GetCartItems(); 
                foreach (var cartItem in myCart) 
                { 
                    for (int i = 0; i < CartItemCount; i++) 
                    { 
                        if (cartItem.Product.ProductID == CartItemUpdates[i].ProductId) 
                        { 
                            if (CartItemUpdates[i].PurchaseQuantity < 1 || CartItemUpdates[i].RemoveItem) 
                            { 
                                RemoveItem(cartId, cartItem.ProductId); 
                            } 
                            else 
                            { 
                                UpdateItem(cartId, cartItem.ProductId, CartItemUpdates[i].PurchaseQuantity); 
                            } 
                        } 
                    } 
                } 
            } 
            catch (Exception exp) 
            { 
                _logger.LogError(exp, $"ERROR: Unable to Update Cart Database - {exp.Message}"); 
                throw new Exception("ERROR: Unable to Update Cart Database.", exp); 
            } 
        } 
        public void RemoveItem(string removeCartID, int removeProductID) 
        { 
            try 
            { 
                var myItem = _db.ShoppingCartItems.SingleOrDefault(c => c.CartId == removeCartID && c.Product.ProductID == removeProductID); 
                if (myItem != null) 
                { 
                    _db.ShoppingCartItems.Remove(myItem); 
                    _db.SaveChanges(); 
                    _logger.LogInformation($"Product with ID {removeProductID} removed from cart successfully."); 
                } 
            } 
            catch (Exception exp) 
            { 
                _logger.LogError(exp, $"ERROR: Unable to Remove Cart Item - {exp.Message}"); 
                throw new Exception("ERROR: Unable to Remove Cart Item.", exp); 
            } 
        } 
        public void UpdateItem(string updateCartID, int updateProductID, int quantity) 
        { 
            try 
            { 
                var myItem = _db.ShoppingCartItems.SingleOrDefault(c => c.CartId == updateCartID && c.Product.ProductID == updateProductID); 
                if (myItem != null) 
                { 
                    myItem.Quantity = quantity; 
                    _db.SaveChanges(); 
                    _logger.LogInformation($"Product with ID {updateProductID} updated to quantity {quantity}."); 
                } 
            } 
            catch (Exception exp) 
            { 
                _logger.LogError(exp, $"ERROR: Unable to Update Cart Item - {exp.Message}"); 
                throw new Exception("ERROR: Unable to Update Cart Item.", exp); 
            } 
        } 
        public void EmptyCart() 
        { 
            try 
            { 
                ShoppingCartId = GetCartId(); 
                var cartItems = _db.ShoppingCartItems.Where(c => c.CartId == ShoppingCartId); 
                foreach (var cartItem in cartItems) 
                { 
                    _db.ShoppingCartItems.Remove(cartItem); 
                } 
                _db.SaveChanges(); 
                _logger.LogInformation("Cart emptied successfully."); 
            } 
            catch (Exception exp) 
            { 
                _logger.LogError(exp, $"ERROR: Unable to Empty Cart - {exp.Message}"); 
                throw new Exception("ERROR: Unable to Empty Cart.", exp); 
            } 
        } 
        public int GetCount() 
        { 
            ShoppingCartId = GetCartId(); 
            int? count = (from cartItems in _db.ShoppingCartItems 
                          where cartItems.CartId == ShoppingCartId 
                          select (int?)cartItems.Quantity).Sum(); 
            return count ?? 0; 
        } 
        public struct ShoppingCartUpdates 
        { 
            public int ProductId; 
            public int PurchaseQuantity; 
            public bool RemoveItem; 
        } 
        public void MigrateCart(string cartId, string userName) 
        { 
            try 
            { 
                var shoppingCart = _db.ShoppingCartItems.Where(c => c.CartId == cartId); 
                foreach (CartItem item in shoppingCart) 
                { 
                    item.CartId = userName; 
                } 
                _httpContextAccessor.HttpContext.Session.SetString(CartSessionKey, userName); 
                _db.SaveChanges(); 
                _logger.LogInformation($"Cart migrated from {cartId} to {userName}."); 
            } 
            catch (Exception exp) 
            { 
                _logger.LogError(exp, $"ERROR: Unable to Migrate Cart - {exp.Message}"); 
                throw new Exception("ERROR: Unable to Migrate Cart.", exp); 
            } 
        } 
    } 
}