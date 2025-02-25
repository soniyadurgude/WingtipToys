using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Web; 
using System.Web.UI; 
using System.Web.UI.WebControls; 
using WingtipToys.Models; 
using System.Web.ModelBinding; 
using System.Diagnostics; 
namespace WingtipToys 
{ 
    public partial class ProductDetails : System.Web.UI.Page 
    { 
        protected void Page_Load(object sender, EventArgs e) 
        { 
            // Log page load event 
            Debug.WriteLine("ProductDetails page loaded."); 
        } 
        public IQueryable<Product> GetProduct([QueryString("ProductID")] int? productId) 
        { 
            var _db = new WingtipToys.Models.ProductContext(); 
            IQueryable<Product> query = _db.Products; 
            if (productId.HasValue && productId > 0) 
            { 
                query = query.Where(p => p.ProductID == productId); 
                Debug.WriteLine($"Product with ID {productId} retrieved successfully."); 
            } 
            else 
            { 
                query = null; 
                Debug.WriteLine("No valid ProductID provided."); 
            } 
            return query; 
        } 
    } 
}