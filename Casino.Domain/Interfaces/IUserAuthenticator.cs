using Casino.Domain.Models;

namespace Casino.Domain.Interfaces;

public interface IUserAuthenticator
{
    Task<AuthenticatedUser?> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default);
}
