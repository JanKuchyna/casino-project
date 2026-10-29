using Casino.Domain.Entities;
using Casino.Domain.Enums;
using Casino.Domain.Interfaces;
using Casino.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Casino.Infrastructure.Repositories;

public class TicketRepository : Repository<Ticket>, ITicketRepository
{
    public TicketRepository(CasinoDbContext context) : base(context)
    {
    }

    public async Task<Ticket?> GetWithItemsAsync(int id, CancellationToken cancellationToken = default)
        => await Set
            .Include(t => t.Items).ThenInclude(i => i.Odd).ThenInclude(o => o.SportEvent)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Ticket>> GetByUserAsync(string userId, CancellationToken cancellationToken = default)
        => await Set.AsNoTracking()
            .Include(t => t.Items).ThenInclude(i => i.Odd).ThenInclude(o => o.SportEvent)
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.PlacedAt)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Ticket>> GetByBookmakerAsync(string bookmakerId, CancellationToken cancellationToken = default)
        => await Set.AsNoTracking()
            .Include(t => t.Items)
            .Where(t => t.BookmakerId == bookmakerId)
            .OrderByDescending(t => t.PlacedAt)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Ticket>> GetPendingBySportEventAsync(int sportEventId, CancellationToken cancellationToken = default)
        => await Set
            .Include(t => t.Items).ThenInclude(i => i.Odd).ThenInclude(o => o.SportEvent)
            .Where(t => t.Status == TicketStatus.Pending
                && t.Items.Any(i => i.Odd.SportEventId == sportEventId))
            .ToListAsync(cancellationToken);
}
