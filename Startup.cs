using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Hosting; 
namespace WingtipToys 
{ 
    public class Startup 
    { 
        public void ConfigureServices(IServiceCollection services) 
        { 
            services.AddCors(options => 
            { 
                options.AddPolicy("AllowAllOrigins", 
                    builder => builder.AllowAnyOrigin() 
                                      .AllowAnyMethod() 
                                      .AllowAnyHeader()); 
            }); 
            // Other service configurations... 
        } 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        { 
            if (env.IsDevelopment()) 
            { 
                app.UseDeveloperExceptionPage(); 
            } 
            else 
            { 
                app.UseExceptionHandler("/Home/Error"); 
                app.UseHsts(); 
            } 
            app.UseHttpsRedirection(); 
            app.UseStaticFiles(); 
            app.UseRouting(); 
            app.UseCors("AllowAllOrigins"); 
            app.UseAuthorization(); 
            app.UseEndpoints(endpoints => 
            { 
                endpoints.MapControllers(); 
            }); 
            // Other middleware... 
        } 
    } 
} using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Hosting; 
using Microsoft.Extensions.Logging; 
public class Startup 
{ 
    public void ConfigureServices(IServiceCollection services) 
    { 
        services.AddControllersWithViews(); 
        // Add any other necessary services here 
    } 
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger) 
    { 
        if (env.IsDevelopment()) 
        { 
            app.UseDeveloperExceptionPage(); 
            logger.LogInformation("Development environment: Developer exception page enabled."); 
        } 
        else 
        { 
            app.UseExceptionHandler("/Home/Error"); 
            app.UseHsts(); 
            logger.LogInformation("Production environment: Exception handler and HSTS enabled."); 
        } 
        app.UseHttpsRedirection(); 
        app.UseStaticFiles(); 
        app.UseRouting(); 
        app.UseAuthentication(); 
        app.UseAuthorization(); 
        app.UseEndpoints(endpoints => 
        { 
            endpoints.MapControllerRoute( 
                name: "default", 
                pattern: "{controller=Home}/{action=Index}/{id?}"); 
            // Serve React app 
            endpoints.MapFallbackToFile("/react/{*path:nonfile}", "index.html"); 
            logger.LogInformation("React app fallback route configured."); 
        }); 
        logger.LogInformation("Application configured successfully."); 
    } 
} 