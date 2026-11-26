using Casino.Domain.Enums;
using Casino.Domain.Interfaces;

namespace Casino.Application.Auth;

public class RegisterUseCase : IRegisterUseCase
{
    private const int MinimumAge = 18;

    private readonly IUserRegistrar _registrar;

    public RegisterUseCase(IUserRegistrar registrar)
    {
        _registrar = registrar;
    }

    // Veřejná registrace vytváří vždy jen zákaznický účet.
    // Bookmakery zakládá admin, admin je jediný a vzniká v seederu.
    public async Task<RegisterResult> RegisterCustomerAsync(
        string email,
        string password,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        CancellationToken cancellationToken = default)
    {
        if (CalculateAge(dateOfBirth, DateTime.Today) < MinimumAge)
        {
            return RegisterResult.Failure($"Registrace je povolena až od {MinimumAge} let.");
        }

        var result = await _registrar.CreateUserAsync(
            email, password, firstName, lastName, dateOfBirth, UserRole.Customer, cancellationToken);

        return result.Succeeded
            ? RegisterResult.Success(result.User!)
            : RegisterResult.Failure(result.Errors);
    }

    private static int CalculateAge(DateTime dateOfBirth, DateTime today)
    {
        var age = today.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
