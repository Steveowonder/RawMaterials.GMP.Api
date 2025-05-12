namespace RawMaterials.GMP.Api.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("[controller]")]
[ApiExplorerSettings(GroupName = "admin")]
public class AdminController(
    IGrantApplicationService _grantApplicationService,
    ILogger<AdminController> _logger) : ControllerBase
{
    [HttpGet("GetAll")]
    public Task<IActionResult> GetAll()
    {
        throw new NotImplementedException();
    }

    [HttpPut("Approve")]
    public async Task<IActionResult> Approve(Guid applicationId)
    {
        try
        {
            // TODO: get user from token 
            var userId = Guid.NewGuid();
            await _grantApplicationService.SetStatus(applicationId, GrantApplicationStatus.Approved, userId);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error approving grant application with ID {ApplicationId}", applicationId);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}
