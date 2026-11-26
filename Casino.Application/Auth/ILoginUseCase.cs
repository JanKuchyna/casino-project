namespace Casino.Application.Auth;

public interface ILoginUseCase
{
    Task<LoginResult> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
}
