using Casino.Domain.Enums;
using Casino.Domain.Interfaces;

namespace Casino.Domain.Entities;

public class Ticket : IEntity
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string BookmakerId { get; set; } = string.Empty;
    public DateTime PlacedAt { get; set; }
    public decimal Stake { get; set; }
    public TicketStatus Status { get; set; } = TicketStatus.Pending;

    public ApplicationUser User { get; set; } = null!;
    public ApplicationUser Bookmaker { get; set; } = null!;
    public ICollection<TicketItem> Items { get; set; } = new List<TicketItem>();
}
