using Casino.Domain.Interfaces;

namespace Casino.Application.Auth;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUserAuthenticator _authenticator;

    public LoginUseCase(IUserAuthenticator authenticator)
    {
        _authenticator = authenticator;
    }

    public async Task<LoginResult> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _authenticator.AuthenticateAsync(email, password, cancellationToken);
        if (user is null)
        {
            return LoginResult.Failure("Neplatný e-mail nebo heslo.");
        }

        if (user.IsBlocked)
        {
            return LoginResult.Failure("Účet je zablokovaný.");
        }

        return LoginResult.Success(user);
    }
}
