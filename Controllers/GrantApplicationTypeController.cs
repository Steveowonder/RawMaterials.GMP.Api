namespace RawMaterials.GMP.Api.Controllers;

[ApiController]
[Route("[controller]")]
[ApiExplorerSettings(GroupName = "grantapplicationtype")]
public class GrantApplicationTypeController(
    IGrantApplicationTypeService _grantApplicationTypeService,
    ILogger<GrantApplicationTypeController> _logger) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet("GetAll")]
    public Task<IActionResult> GetAll()
    {
        throw new NotImplementedException();
    }
}
