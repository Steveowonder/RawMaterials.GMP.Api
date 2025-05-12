namespace RawMaterials.GMP.Api.Controllers;

[ApiController]
[Route("[controller]")]
[ApiExplorerSettings(GroupName = "grantapplicationtype")]
public class GrantApplicationTypeController(
    IGrantApplicationTypeService _grantApplicationTypeService,
    ILogger<GrantApplicationTypeController> _logger) : ControllerBase
{
    //[Authorize(Roles = "Admin")]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var grantApplicationTypes = await _grantApplicationTypeService.GetAll();
            return Ok(grantApplicationTypes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error when loading Grant Application Types. {ex.Message}");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}
