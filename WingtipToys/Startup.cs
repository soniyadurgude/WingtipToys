using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.IdentityModel.Tokens; 
using System.Text; 
using Microsoft.Extensions.Configuration; 
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Hosting; 
using System; 
namespace WingtipToys 
{ 
    public class Startup 
    { 
        public IConfiguration Configuration { get; } 
        public Startup(IConfiguration configuration) 
        { 
            Configuration = configuration; 
        } 
        // This method gets called by the runtime. Use this method to add services to the container. 
        public void ConfigureServices(IServiceCollection services) 
        { 
            services.AddControllers(); 
            // Configure JWT Authentication 
            var jwtSettings = Configuration.GetSection("Jwt"); 
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]); 
            services.AddAuthentication(options => 
            { 
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
            }) 
            .AddJwtBearer(options => 
            { 
                options.TokenValidationParameters = new TokenValidationParameters 
                { 
                    ValidateIssuer = true, 
                    ValidateAudience = true, 
                    ValidateLifetime = true, 
                    ValidateIssuerSigningKey = true, 
                    ValidIssuer = jwtSettings["Issuer"], 
                    ValidAudience = jwtSettings["Audience"], 
                    IssuerSigningKey = new SymmetricSecurityKey(key) 
                }; 
            }); 
            // Register JWT Token Service 
            services.AddSingleton<JwtTokenService>(); 
        } 
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
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
            app.UseAuthentication(); 
            app.UseAuthorization(); 
            app.UseEndpoints(endpoints => 
            { 
                endpoints.MapControllers(); 
            }); 
        } 
    } 
} 