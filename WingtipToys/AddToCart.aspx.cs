using System; 
using System.Diagnostics; 
using System.Web; 
using System.Web.UI; 
namespace WingtipToys 
{ 
    public partial class AddToCart : System.Web.UI.Page 
    { 
        protected void Page_Load(object sender, EventArgs e) 
        { 
            string rawId = Request.QueryString["ProductID"]; 
            int productId; 
            if (!String.IsNullOrEmpty(rawId) && int.TryParse(rawId, out productId)) 
            { 
                try 
                { 
                    // Redirect to the new API endpoint for adding to cart 
                    var apiUrl = $"/api/ShoppingCart/AddToCart?productId={productId}"; 
                    Response.Redirect(apiUrl, false); 
                    Context.ApplicationInstance.CompleteRequest(); 
                    Debug.WriteLine($"Redirected to API for adding product with ID {productId} to cart."); 
                } 
                catch (Exception ex) 
                { 
                    // Log the error with full stack trace 
                    Debug.WriteLine($"ERROR: Unable to redirect to API for adding product to cart. {ex.Message} \n {ex.StackTrace}"); 
                    throw new Exception("ERROR: Unable to redirect to API for adding product to cart.", ex); 
                } 
            } 
            else 
            { 
                Debug.Fail("ERROR : We should never get to AddToCart.aspx without a ProductId."); 
                throw new Exception("ERROR : It is illegal to load AddToCart.aspx without setting a ProductId."); 
            } 
        } 
    } 
}