using Microsoft.AspNetCore.Identity;
using SureProfit.Application.AuthorizationManagement;

namespace SureProfit.Infraestructure.Identity;

public class IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

    public async Task<IdentityResultDto> CreateUserAsync(RegisterUserDto registerUserDto)
    {
        var user = new ApplicationUser
        {
            UserName = registerUserDto.Email,
            Email = registerUserDto.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, registerUserDto.Password);

        return result.ToIdentityResultDto();
    }

    public async Task<IdentityResultDto> Login(LoginUserDto loginUserDto)
    {
        var result = await _signInManager.PasswordSignInAsync(loginUserDto.Email, loginUserDto.Password,
            isPersistent: false, lockoutOnFailure: true);

        return result.ToIdentityResultDto();
    }
}
