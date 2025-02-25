using System; 
using System.Collections.Generic; 
using System.Web; 
using System.Web.Routing; 
using Microsoft.AspNet.FriendlyUrls; 
namespace WingtipToys 
{ 
    public static class RouteConfig 
    { 
        public static void RegisterRoutes(RouteCollection routes) 
        { 
            var settings = new FriendlyUrlSettings(); 
            settings.AutoRedirectMode = RedirectMode.Permanent; 
            routes.EnableFriendlyUrls(settings); 
            // Add route for React components 
            routes.MapPageRoute( 
                routeName: "ReactRoute", 
                routeUrl: "react/{*pathInfo}", 
                physicalFile: "~/index.html" 
            ); 
        } 
    } 
}