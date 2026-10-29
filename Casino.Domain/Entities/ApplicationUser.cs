using Casino.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Casino.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public UserRole Role { get; set; } = UserRole.Customer;
    public bool IsBlocked { get; set; }

    public Wallet? Wallet { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public ICollection<SportEvent> ManagedSportEvents { get; set; } = new List<SportEvent>();
    public ICollection<Ticket> ManagedTickets { get; set; } = new List<Ticket>();
}
