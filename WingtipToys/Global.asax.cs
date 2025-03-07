using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Web; 
using System.Web.Optimization; 
using System.Web.Routing; 
using System.Web.Security; 
using System.Web.SessionState; 
using System.Data.Entity; 
using WingtipToys.Models; 
namespace WingtipToys 
{ 
    public class Global : HttpApplication 
    { 
        void Application_Start(object sender, EventArgs e) 
        { 
            // Code that runs on application startup 
            RouteConfig.RegisterRoutes(RouteTable.Routes); 
            BundleConfig.RegisterBundles(BundleTable.Bundles); 
            // Initialize the product database. 
            Database.SetInitializer(new ProductDatabaseInitializer()); 
            // Log application start 
            System.Diagnostics.Debug.WriteLine("Application started and database initialized."); 
        } 
    } 
}