using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V1;

[ApiController]
[Route(Routes.Base)]
[ApiVersion("1.0")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello World!");
    }
}