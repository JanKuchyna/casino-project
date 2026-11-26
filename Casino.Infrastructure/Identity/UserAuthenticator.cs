using Casino.Domain.Interfaces;
using Casino.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Casino.Infrastructure.Identity;

public class UserAuthenticator : IUserAuthenticator
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserAuthenticator(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AuthenticatedUser?> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, password))
        {
            return null;
        }

        return new AuthenticatedUser(user.Id, user.Email!, user.FirstName, user.LastName, user.Role, user.IsBlocked);
    }
}
