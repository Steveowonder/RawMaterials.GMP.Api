namespace RawMaterials.GMP.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(ILogger<AccountController> _logger) : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest model)
    {
        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest model)
    {
        return Ok();
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        return Ok("Logged out successfully.");
    }

    [HttpPost("change-password")]
    [Authorize]
    public IActionResult ChangePassword(ChangePasswordRequest model)
    {
        return Ok("Password changed successfully.");
    }
}
