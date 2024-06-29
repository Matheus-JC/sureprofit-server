namespace SureProfit.Application.AuthorizationManagement;

public class IdentityResultDto(bool succeeded, IEnumerable<string> errors)
{
    public bool Succeeded { get; init; } = succeeded;
    public string[] Errors { get; init; } = errors.ToArray();
}
