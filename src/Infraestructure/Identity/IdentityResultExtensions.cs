using Microsoft.AspNetCore.Identity;
using SureProfit.Application.AuthorizationManagement;

namespace SureProfit.Infraestructure.Identity;

public static class IdentityResultExtensions
{
    public static IdentityResultDto ToIdentityResultDto(this IdentityResult identityResult) =>
        new(identityResult.Succeeded, identityResult.Errors.Select(e => e.Description));

    public static IdentityResultDto ToIdentityResultDto(this SignInResult signInResult) =>
        new(signInResult.Succeeded, []);

}
