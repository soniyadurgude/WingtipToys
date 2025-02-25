using Microsoft.EntityFrameworkCore; 
using WingtipToys.Data; 
var builder = WebApplication.CreateBuilder(args); 
// Add services to the container. 
builder.Services.AddDbContext<ProductContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("WingtipToys"))); 
builder.Services.AddControllers(); 
var app = builder.Build(); 
// Configure the HTTP request pipeline. 
app.UseHttpsRedirection(); 
app.UseAuthorization(); 
app.MapControllers(); 
app.Run(); using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.IdentityModel.Tokens; 
using System.Text; 
var builder = WebApplication.CreateBuilder(args); 
// Add services to the container. 
builder.Services.AddControllers(); 
// Configure JWT Authentication 
var jwtSettings = builder.Configuration.GetSection("Jwt"); 
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]); 
builder.Services.AddAuthentication(options => 
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
builder.Services.AddSingleton<JwtTokenService>(); 
var app = builder.Build(); 
// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment()) 
{ 
    app.UseDeveloperExceptionPage(); 
} 
app.UseHttpsRedirection(); 
app.UseAuthentication(); 
app.UseAuthorization(); 
app.MapControllers(); 
app.Run(); var builder = WebApplication.CreateBuilder(args); 
// Add services to the container. 
builder.Services.AddControllers(); 
builder.Services.AddDbContext<ProductContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("WingtipToys"))); 
// Build the application. 
var app = builder.Build(); 
// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment()) 
{ 
    app.UseDeveloperExceptionPage(); 
} 
app.UseHttpsRedirection(); 
app.UseAuthorization(); 
app.MapControllers(); 
app.Run(); 