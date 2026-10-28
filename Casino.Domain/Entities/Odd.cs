using Casino.Domain.Enums;
using Casino.Domain.Interfaces;

namespace Casino.Domain.Entities;

public class Odd : IEntity
{
    public int Id { get; set; }
    public int SportEventId { get; set; }
    public BetType BetType { get; set; }
    public decimal Value { get; set; }

    public SportEvent SportEvent { get; set; } = null!;
    public ICollection<TicketItem> TicketItems { get; set; } = new List<TicketItem>();
}
