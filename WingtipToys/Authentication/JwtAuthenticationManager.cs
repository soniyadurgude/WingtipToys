using Microsoft.Extensions.Configuration; 
using Microsoft.IdentityModel.Tokens; 
using System; 
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims; 
using System.Text; 
namespace WingtipToys.Authentication 
{ 
    public class JwtAuthenticationManager 
    { 
        private readonly IConfiguration _configuration; 
        public JwtAuthenticationManager(IConfiguration configuration) 
        { 
            _configuration = configuration; 
        } 
        public string GenerateToken(string username) 
        { 
            try 
            { 
                var jwtSettings = _configuration.GetSection("Jwt"); 
                var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]); 
                var tokenHandler = new JwtSecurityTokenHandler(); 
                var tokenDescriptor = new SecurityTokenDescriptor 
                { 
                    Subject = new ClaimsIdentity(new Claim[] 
                    { 
                        new Claim(ClaimTypes.Name, username) 
                    }), 
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresInMinutes"])), 
                    Issuer = jwtSettings["Issuer"], 
                    Audience = jwtSettings["Audience"], 
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) 
                }; 
                var token = tokenHandler.CreateToken(tokenDescriptor); 
                var tokenString = tokenHandler.WriteToken(token); 
                Console.WriteLine($"Token generated successfully for user: {username}"); 
                return tokenString; 
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine($"Error generating token for user: {username}. Error: {ex.Message}"); 
                Console.WriteLine(ex.ToString()); 
                throw; 
            } 
        } 
    } 
} 