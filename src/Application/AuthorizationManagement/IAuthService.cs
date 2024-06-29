namespace SureProfit.Application.AuthorizationManagement;

public interface IAuthService
{
    Task<string?> Register(RegisterUserDto registerUserDto);
    Task<string?> Login(LoginUserDto loginUserDto);
}
