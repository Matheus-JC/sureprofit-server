
using AutoMapper;
using SureProfit.Application.Notifications;
using SureProfit.Domain.Interfaces;

namespace SureProfit.Application.AuthorizationManagement;

public class AuthService(
    IIdentityService identityService,
    IAuthTokenGenerator authTokenGenerator,
    IUnitOfWork unitOfWork,
    INotifier notifier,
    IMapper mapper
) : BaseService(unitOfWork, notifier, mapper), IAuthService
{
    private readonly IIdentityService _identityService = identityService;
    private readonly IAuthTokenGenerator _authTokenGenerator = authTokenGenerator;

    public async Task<string?> Register(RegisterUserDto registerUserDto)
    {
        if (!await Validate(new RegisterUserDtoValidator(), registerUserDto))
        {
            return null;
        }

        var result = await _identityService.CreateUserAsync(registerUserDto);

        if (result.Succeeded)
        {
            await _identityService.Login(new LoginUserDto { Email = registerUserDto.Email, Password = registerUserDto.Password });
            return _authTokenGenerator.Generate();
        }

        Notify(result.Errors);

        return null;
    }

    public async Task<string?> Login(LoginUserDto loginUserDto)
    {
        if (!await Validate(new LoginUserDtoValidator(), loginUserDto))
        {
            return null;
        }

        var result = await _identityService.Login(loginUserDto);

        if (result.Succeeded)
        {
            return _authTokenGenerator.Generate();
        }

        Notify("Incorrect username or password");

        return null;
    }
}
