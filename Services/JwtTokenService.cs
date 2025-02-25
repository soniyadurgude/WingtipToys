using Microsoft.Extensions.Configuration; 
using Microsoft.IdentityModel.Tokens; 
using System; 
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims; 
using System.Text; 
using Microsoft.Extensions.Logging; 
public class JwtTokenService 
{ 
    private readonly IConfiguration _configuration; 
    private readonly ILogger<JwtTokenService> _logger; 
    public JwtTokenService(IConfiguration configuration, ILogger<JwtTokenService> logger) 
    { 
        _configuration = configuration; 
        _logger = logger; 
    } 
    public string GenerateToken(string username) 
    { 
        try 
        { 
            _logger.LogInformation("Generating JWT token for user: {Username}", username); 
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
            _logger.LogInformation("JWT token generated successfully for user: {Username}", username); 
            return tokenString; 
        } 
        catch (Exception ex) 
        { 
            _logger.LogError(ex, "Error occurred while generating JWT token for user: {Username}", username); 
            throw; 
        } 
    } 
} 