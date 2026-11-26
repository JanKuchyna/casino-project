using Casino.Domain.Models;

namespace Casino.Application.Auth;

public record LoginResult(bool Succeeded, string? Error, AuthenticatedUser? User)
{
    public static LoginResult Success(AuthenticatedUser user) => new(true, null, user);
    public static LoginResult Failure(string error) => new(false, error, null);
}
