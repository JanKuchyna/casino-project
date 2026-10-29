using Casino.Domain.Entities;

namespace Casino.Domain.Interfaces;

public interface ITicketRepository : IRepository<Ticket>
{
    Task<Ticket?> GetWithItemsAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Ticket>> GetByUserAsync(string userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Ticket>> GetByBookmakerAsync(string bookmakerId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Ticket>> GetPendingBySportEventAsync(int sportEventId, CancellationToken cancellationToken = default);
}
