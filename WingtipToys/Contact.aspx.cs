using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Web; 
using System.Web.UI; 
using System.Web.UI.WebControls; 
namespace WingtipToys 
{ 
    public partial class Contact : Page 
    { 
        protected void Page_Load(object sender, EventArgs e) 
        { 
            // Log page load event 
            System.Diagnostics.Debug.WriteLine("Contact page loaded."); 
        } 
    } 
}