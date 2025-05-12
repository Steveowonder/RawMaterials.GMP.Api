namespace RawMaterials.GMP.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GrantApplicationController(
    IGrantApplicationService _grantApplicationService,
    ILogger<GrantApplicationController> _logger) : ControllerBase
{
    [HttpGet("get-all")]
    public Task<IActionResult> GetAll()
    {
        throw new NotImplementedException();
    }

    [HttpPut("approve")]
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
