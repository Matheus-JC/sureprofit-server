using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CompanyController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("VERSÃO 1.0");
    }
}
