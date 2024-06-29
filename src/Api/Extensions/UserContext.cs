using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SureProfit.Application.AuthorizationManagement;

namespace SureProfit.Api.Extensions;

public class UserContext(IHttpContextAccessor acessor) : IUserContext
{
    private readonly IHttpContextAccessor _accessor = acessor;

    public Guid GetUserId()
    {
        if (!IsAuthenticated())
        {
            return Guid.Empty;
        }

        var claim = _accessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        return claim is null ? Guid.Empty : Guid.Parse(claim);
    }

    public string GetUserName()
    {
        var userName = _accessor.HttpContext?.User.FindFirst("userName")?.Value;
        if (!string.IsNullOrEmpty(userName)) return userName;

        userName = _accessor.HttpContext?.User.Identity?.Name;
        if (!string.IsNullOrEmpty(userName)) return userName;

        userName = _accessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
        if (!string.IsNullOrEmpty(userName)) return userName;

        userName = _accessor.HttpContext?.User.FindFirst(ClaimTypes.GivenName)?.Value;
        if (!string.IsNullOrEmpty(userName)) return userName;

        var sub = _accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        if (sub != null) return sub.Value;

        return string.Empty;
    }

    public bool IsAuthenticated()
    {
        return _accessor.HttpContext?.User.Identity is { IsAuthenticated: true };
    }

    public bool IsInRole(string role)
    {
        return _accessor.HttpContext != null && _accessor.HttpContext.User.IsInRole(role);
    }

    public string? GetRemoteIpAddress()
    {
        return _accessor.HttpContext?.Connection.LocalIpAddress?.ToString();
    }

    public string? GetLocalIpAddress()
    {
        return _accessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
    }
}
