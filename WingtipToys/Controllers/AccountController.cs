using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Configuration; 
using Microsoft.AspNetCore.Authorization; 
using System; 
using System.Threading.Tasks; 
using Microsoft.Extensions.Logging; 
namespace WingtipToys.Controllers 
{ 
    [ApiController] 
    [Route("api/[controller]")] 
    public class AccountController : ControllerBase 
    { 
        private readonly JwtTokenService _jwtTokenService; 
        private readonly ILogger<AccountController> _logger; 
        public AccountController(JwtTokenService jwtTokenService, ILogger<AccountController> logger) 
        { 
            _jwtTokenService = jwtTokenService; 
            _logger = logger; 
        } 
        [HttpPost("login")] 
        public async Task<IActionResult> Login([FromBody] LoginRequest request) 
        { 
            try 
            { 
                _logger.LogInformation("Login attempt for user: {Username}", request.Username); 
                // Replace this with your actual user validation logic 
                if (request.Username == "test" && request.Password == "password") 
                { 
                    var token = _jwtTokenService.GenerateToken(request.Username); 
                    _logger.LogInformation("Token generated for user: {Username}", request.Username); 
                    return Ok(new { Token = token }); 
                } 
                _logger.LogWarning("Unauthorized login attempt for user: {Username}", request.Username); 
                return Unauthorized(); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred during login for user: {Username}", request.Username); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
        [Authorize] 
        [HttpGet("profile")] 
        public IActionResult GetProfile() 
        { 
            try 
            { 
                var username = User.Identity.Name; 
                _logger.LogInformation("Profile accessed for user: {Username}", username); 
                // Replace this with actual profile retrieval logic 
                var profile = new { Username = username, Email = $"{username}@example.com" }; 
                return Ok(profile); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "An error occurred while retrieving profile for user: {Username}", User.Identity.Name); 
                return StatusCode(500, "Internal server error"); 
            } 
        } 
    } 
    public class LoginRequest 
    { 
        public string Username { get; set; } 
        public string Password { get; set; } 
    } 
} 