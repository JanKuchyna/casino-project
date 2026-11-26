using Casino.Domain.Enums;
using Casino.Domain.Models;

namespace Casino.Domain.Interfaces;

public interface IUserRegistrar
{
    // Vytvoří uživatele s danou rolí (Customer nebo Bookmaker) včetně prázdné peněženky.
    // Roli Admin vytvořit nelze – admin je v systému jen jeden a zakládá ho seeder.
    Task<UserCreationResult> CreateUserAsync(
        string email,
        string password,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        UserRole role,
        CancellationToken cancellationToken = default);
}
