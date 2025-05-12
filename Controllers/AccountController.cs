namespace RawMaterials.GMP.Api.Controllers;

[ApiController]
[Route("[controller]")]
[ApiExplorerSettings(GroupName = "account")]
public class AccountController(ILogger<AccountController> _logger) : ControllerBase
{
    [HttpPost("Register")]
    public IActionResult Register(RegisterRequest model)
    {
        return Ok("User registered successfully.");
    }

    [HttpPost("Login")]
    public IActionResult Login(LoginRequest model)
    {
        return Ok();
    }

    [HttpPost("Logout")]
    [Authorize]
    public IActionResult Logout()
    {
        return Ok("Logged out successfully.");
    }

    [HttpPost("ChangePassword")]
    [Authorize]
    public IActionResult ChangePassword(ChangePasswordRequest model)
    {
        return Ok("Password changed successfully.");
    }
}
