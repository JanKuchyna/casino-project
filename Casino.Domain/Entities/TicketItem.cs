using Casino.Domain.Interfaces;

namespace Casino.Domain.Entities;

public class TicketItem : IEntity
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public int OddId { get; set; }

    public decimal OddSnapshot { get; set; }

    public Ticket Ticket { get; set; } = null!;
    public Odd Odd { get; set; } = null!;
}
