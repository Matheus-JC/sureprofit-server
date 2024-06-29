namespace SureProfit.Application.AuthorizationManagement;

public interface IUserContext
{
    Guid GetUserId();
    string GetUserName();
    bool IsAuthenticated();
    bool IsInRole(string role);
    string? GetRemoteIpAddress();
    string? GetLocalIpAddress();
}
