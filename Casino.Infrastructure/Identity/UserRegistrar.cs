using Casino.Domain.Entities;
using Casino.Domain.Enums;
using Casino.Domain.Interfaces;
using Casino.Domain.Models;
using Casino.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Casino.Infrastructure.Identity;

public class UserRegistrar : IUserRegistrar
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly CasinoDbContext _context;

    public UserRegistrar(UserManager<ApplicationUser> userManager, CasinoDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<UserCreationResult> CreateUserAsync(
        string email,
        string password,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        UserRole role,
        CancellationToken cancellationToken = default)
    {
        if (role == UserRole.Admin)
        {
            throw new InvalidOperationException("Admin účet je v systému jen jeden a nelze vytvořit další.");
        }

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dateOfBirth,
            Role = role
        };

        // Uživatel a jeho peněženka vzniknou buď oba, nebo ani jeden.
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            return UserCreationResult.Failure(result.Errors.Select(Translate).Distinct().ToList());
        }

        _context.Wallets.Add(new Wallet { UserId = user.Id, Balance = 0m });
        await _context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return UserCreationResult.Success(
            new AuthenticatedUser(user.Id, user.Email!, user.FirstName, user.LastName, user.Role, user.IsBlocked));
    }

    private string Translate(IdentityError error) => error.Code switch
    {
        nameof(IdentityErrorDescriber.DuplicateEmail) or nameof(IdentityErrorDescriber.DuplicateUserName)
            => "Účet s tímto e-mailem už existuje.",
        nameof(IdentityErrorDescriber.InvalidEmail) or nameof(IdentityErrorDescriber.InvalidUserName)
            => "Neplatný e-mail.",
        nameof(IdentityErrorDescriber.PasswordTooShort)
            => $"Heslo musí mít alespoň {_userManager.Options.Password.RequiredLength} znaky.",
        nameof(IdentityErrorDescriber.PasswordRequiresDigit)
            => "Heslo musí obsahovat alespoň jednu číslici.",
        _ => error.Description
    };
}
