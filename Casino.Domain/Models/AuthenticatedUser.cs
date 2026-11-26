using Casino.Domain.Enums;

namespace Casino.Domain.Models;

public record AuthenticatedUser(
    string Id,
    string Email,
    string FirstName,
    string LastName,
    UserRole Role,
    bool IsBlocked);
