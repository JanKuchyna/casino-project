namespace Casino.Application.Auth;

public interface IRegisterUseCase
{
    Task<RegisterResult> RegisterCustomerAsync(
        string email,
        string password,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        CancellationToken cancellationToken = default);
}
