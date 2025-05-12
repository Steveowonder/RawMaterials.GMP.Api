namespace RawMaterials.GMP.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GrantApplicationTypeController(
    IGrantApplicationTypeService _grantApplicationTypeService,
    ILogger<GrantApplicationTypeController> _logger) : ControllerBase
{
    [HttpGet("get-all")]
    public Task<IActionResult> GetAll()
    {
        throw new NotImplementedException();
    }
}
