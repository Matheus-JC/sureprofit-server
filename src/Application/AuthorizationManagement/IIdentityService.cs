namespace SureProfit.Application.AuthorizationManagement;

public interface IIdentityService
{
    Task<IdentityResultDto> CreateUserAsync(RegisterUserDto registerUserDto);
    Task<IdentityResultDto> Login(LoginUserDto loginUserDto);
}
