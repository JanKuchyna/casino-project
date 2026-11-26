using Casino.Domain.Models;

namespace Casino.Application.Auth;

public record RegisterResult(bool Succeeded, IReadOnlyList<string> Errors, AuthenticatedUser? User)
{
    public static RegisterResult Success(AuthenticatedUser user) => new(true, [], user);
    public static RegisterResult Failure(params string[] errors) => new(false, errors, null);
    public static RegisterResult Failure(IReadOnlyList<string> errors) => new(false, errors, null);
}
