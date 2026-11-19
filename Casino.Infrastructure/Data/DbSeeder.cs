using Casino.Domain.Enums;
using Casino.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Casino.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<CasinoDbContext>();
        await context.Database.MigrateAsync();

        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        await CreateUserAsync(userManager, "admin@casino.cz", "1234", "Adam", "Administrátor", new DateTime(1985, 1, 1), UserRole.Admin);
        await CreateUserAsync(userManager, "bookmaker@casino.cz", "1234", "Božena", "Sázková", new DateTime(1990, 5, 15), UserRole.Bookmaker);
        await CreateUserAsync(userManager, "customer@casino.cz", "1234", "Karel", "Zákazník", new DateTime(1995, 9, 30), UserRole.Customer);
    }

    private static async Task CreateUserAsync(
        UserManager<ApplicationUser> userManager,
        string email,
        string password,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        UserRole role)
    {
        if (await userManager.FindByEmailAsync(email) is not null)
        {
            return;
        }

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dateOfBirth,
            Role = role
        };

        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                $"Seeding user '{email}' failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }
}
