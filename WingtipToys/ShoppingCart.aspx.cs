using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Web; 
using System.Web.UI; 
using System.Web.UI.WebControls; 
using WingtipToys.Models; 
using WingtipToys.Logic; 
using System.Collections.Specialized; 
using System.Collections; 
using System.Web.ModelBinding; 
using System.Diagnostics; 
namespace WingtipToys 
{ 
    public partial class ShoppingCart : System.Web.UI.Page 
    { 
        protected void Page_Load(object sender, EventArgs e) 
        { 
            try 
            { 
                using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions()) 
                { 
                    decimal cartTotal = 0; 
                    cartTotal = usersShoppingCart.GetTotal(); 
                    if (cartTotal > 0) 
                    { 
                        // Display Total. 
                        lblTotal.Text = String.Format("{0:c}", cartTotal); 
                        Debug.WriteLine($"Cart total displayed: {cartTotal}"); 
                    } 
                    else 
                    { 
                        LabelTotalText.Text = ""; 
                        lblTotal.Text = ""; 
                        ShoppingCartTitle.InnerText = "Shopping Cart is Empty"; 
                        UpdateBtn.Visible = false; 
                        Debug.WriteLine("Shopping cart is empty."); 
                    } 
                } 
            } 
            catch (Exception ex) 
            { 
                Debug.WriteLine($"ERROR: Unable to load shopping cart. {ex.Message} \n {ex.StackTrace}"); 
                throw new Exception("ERROR: Unable to load shopping cart.", ex); 
            } 
        } 
        public List<CartItem> GetShoppingCartItems() 
        { 
            try 
            { 
                ShoppingCartActions actions = new ShoppingCartActions(); 
                var items = actions.GetCartItems(); 
                Debug.WriteLine($"Retrieved {items.Count} items from shopping cart."); 
                return items; 
            } 
            catch (Exception ex) 
            { 
                Debug.WriteLine($"ERROR: Unable to retrieve shopping cart items. {ex.Message} \n {ex.StackTrace}"); 
                throw new Exception("ERROR: Unable to retrieve shopping cart items.", ex); 
            } 
        } 
        public List<CartItem> UpdateCartItems() 
        { 
            try 
            { 
                using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions()) 
                { 
                    String cartId = usersShoppingCart.GetCartId(); 
                    ShoppingCartActions.ShoppingCartUpdates[] cartUpdates = new ShoppingCartActions.ShoppingCartUpdates[CartList.Rows.Count]; 
                    for (int i = 0; i < CartList.Rows.Count; i++) 
                    { 
                        IOrderedDictionary rowValues = new OrderedDictionary(); 
                        rowValues = GetValues(CartList.Rows[i]); 
                        cartUpdates[i].ProductId = Convert.ToInt32(rowValues["ProductID"]); 
                        CheckBox cbRemove = new CheckBox(); 
                        cbRemove = (CheckBox)CartList.Rows[i].FindControl("Remove"); 
                        cartUpdates[i].RemoveItem = cbRemove.Checked; 
                        TextBox quantityTextBox = new TextBox(); 
                        quantityTextBox = (TextBox)CartList.Rows[i].FindControl("PurchaseQuantity"); 
                        cartUpdates[i].PurchaseQuantity = Convert.ToInt16(quantityTextBox.Text.ToString()); 
                    } 
                    usersShoppingCart.UpdateShoppingCartDatabase(cartId, cartUpdates); 
                    CartList.DataBind(); 
                    lblTotal.Text = String.Format("{0:c}", usersShoppingCart.GetTotal()); 
                    Debug.WriteLine("Shopping cart updated successfully."); 
                    return usersShoppingCart.GetCartItems(); 
                } 
            } 
            catch (Exception ex) 
            { 
                Debug.WriteLine($"ERROR: Unable to update shopping cart items. {ex.Message} \n {ex.StackTrace}"); 
                throw new Exception("ERROR: Unable to update shopping cart items.", ex); 
            } 
        } 
        public static IOrderedDictionary GetValues(GridViewRow row) 
        { 
            IOrderedDictionary values = new OrderedDictionary(); 
            foreach (DataControlFieldCell cell in row.Cells) 
            { 
                if (cell.Visible) 
                { 
                    // Extract values from the cell. 
                    cell.ContainingField.ExtractValuesFromCell(values, cell, row.RowState, true); 
                } 
            } 
            return values; 
        } 
        protected void UpdateBtn_Click(object sender, EventArgs e) 
        { 
            try 
            { 
                UpdateCartItems(); 
                Debug.WriteLine("Update button clicked and cart items updated."); 
            } 
            catch (Exception ex) 
            { 
                Debug.WriteLine($"ERROR: Unable to update cart on button click. {ex.Message} \n {ex.StackTrace}"); 
                throw new Exception("ERROR: Unable to update cart on button click.", ex); 
            } 
        } 
    } 
}