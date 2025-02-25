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
app.Run(); 