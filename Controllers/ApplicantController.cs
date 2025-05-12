namespace RawMaterials.GMP.Api.Controllers;

[ApiController]
[Authorize(Roles = "Applicant")]
[Route("[controller]")]
[ApiExplorerSettings(GroupName = "applicant")]
public class ApplicantController(
    IGrantApplicationTypeService _grantApplicationTypeService,
    ILogger<ApplicantController> _logger) : ControllerBase
{
    [HttpGet("GetAll")]
    public Task<IActionResult> GetAll()
    {
        throw new NotImplementedException();
    }
}
