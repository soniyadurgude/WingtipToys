using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Logging; 
[ApiController] 
[Route("api/[controller]")] 
public class AuthController : ControllerBase 
{ 
    private readonly JwtTokenService _jwtTokenService; 
    private readonly ILogger<AuthController> _logger; 
    public AuthController(JwtTokenService jwtTokenService, ILogger<AuthController> logger) 
    { 
        _jwtTokenService = jwtTokenService; 
        _logger = logger; 
    } 
    [HttpPost("login")] 
    public IActionResult Login([FromBody] LoginRequest request) 
    { 
        try 
        { 
            // Replace this with your actual user validation logic 
            if (request.Username == "test" && request.Password == "password") 
            { 
                var token = _jwtTokenService.GenerateToken(request.Username); 
                _logger.LogInformation("User {Username} logged in successfully.", request.Username); 
                return Ok(new { Token = token }); 
            } 
            _logger.LogWarning("Unauthorized login attempt for user {Username}.", request.Username); 
            return Unauthorized(); 
        } 
        catch (Exception ex) 
        { 
            _logger.LogError(ex, "An error occurred during login for user {Username}.", request.Username); 
            return StatusCode(500, "Internal server error"); 
        } 
    } 
} 
public class LoginRequest 
{ 
    public string Username { get; set; } 
    public string Password { get; set; } 
} 