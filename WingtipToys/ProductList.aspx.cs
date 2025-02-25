using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Web; 
using System.Web.UI; 
using System.Web.UI.WebControls; 
using WingtipToys.Models; 
using System.Web.ModelBinding; 
using System.Web.Routing; 
using System.Diagnostics; 
namespace WingtipToys 
{ 
    public partial class ProductList : System.Web.UI.Page 
    { 
        protected void Page_Load(object sender, EventArgs e) 
        { 
            // Log page load event 
            Debug.WriteLine("ProductList page loaded."); 
        } 
        public IQueryable<Product> GetProducts( 
                            [QueryString("id")] int? categoryId) 
        { 
            // Redirect to the new API endpoint for retrieving products 
            try 
            { 
                var apiUrl = categoryId.HasValue && categoryId > 0 
                    ? $"/api/Products?categoryId={categoryId}" 
                    : "/api/Products"; 
                Response.Redirect(apiUrl, false); 
                Context.ApplicationInstance.CompleteRequest(); 
                Debug.WriteLine($"Redirected to API for retrieving products with category ID {categoryId}."); 
            } 
            catch (Exception ex) 
            { 
                // Log the error with full stack trace 
                Debug.WriteLine($"ERROR: Unable to redirect to API for retrieving products. {ex.Message} \n {ex.StackTrace}"); 
                throw new Exception("ERROR: Unable to redirect to API for retrieving products.", ex); 
            } 
            return null; // This line will never be reached due to the redirect 
        } 
    } 
}