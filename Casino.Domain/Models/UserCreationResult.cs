namespace Casino.Domain.Models;

public record UserCreationResult(AuthenticatedUser? User, IReadOnlyList<string> Errors)
{
    public bool Succeeded => User is not null;

    public static UserCreationResult Success(AuthenticatedUser user) => new(user, []);
    public static UserCreationResult Failure(IReadOnlyList<string> errors) => new(null, errors);
}
