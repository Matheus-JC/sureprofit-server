using Microsoft.AspNetCore.Mvc;
using SureProfit.Api.Models;
using SureProfit.Application.AuthorizationManagement;
using SureProfit.Application.Notifications;

namespace SureProfit.Api.Controllers.V1;

[Route(Routes.Base)]
public class AuthController(IAuthService authService, INotifier notifier) : MainController(notifier)
{
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterUserDto registerUserDto)
    {
        if (!ModelState.IsValid)
        {
            return HandleBadRequest(ModelState);
        }

        var token = await _authService.Register(registerUserDto);

        if (IsOperationInvalid())
        {
            return HandleBadRequest();
        }

        return Ok(token);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginUserDto loginUserDto)
    {
        if (!ModelState.IsValid)
        {
            return HandleBadRequest(ModelState);
        }

        var token = await _authService.Login(loginUserDto);

        if (IsOperationInvalid())
        {
            return HandleBadRequest();
        }

        return Ok(token);
    }
}
