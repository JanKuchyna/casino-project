using Casino.Domain.Interfaces;

namespace Casino.Domain.Entities;

public class TicketItem : IEntity
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public int OddId { get; set; }

    // Hodnota kurzu v okamžiku podání sázky, pozdější úpravy kurzu ji nemění
    public decimal OddSnapshot { get; set; }

    public Ticket Ticket { get; set; } = null!;
    public Odd Odd { get; set; } = null!;
}
