using Casino.Domain.Entities;
using Casino.Domain.Interfaces;
using Casino.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Casino.Infrastructure.Repositories;

public class SportEventRepository : Repository<SportEvent>, ISportEventRepository
{
    public SportEventRepository(CasinoDbContext context) : base(context)
    {
    }

    public async Task<SportEvent?> GetWithOddsAsync(int id, CancellationToken cancellationToken = default)
        => await Set.Include(s => s.Odds).FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public async Task<IReadOnlyList<SportEvent>> GetActiveAsync(CancellationToken cancellationToken = default)
        => await Set.AsNoTracking()
            .Include(s => s.Odds)
            .Where(s => s.IsActive)
            .OrderBy(s => s.StartTime)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<SportEvent>> GetByBookmakerAsync(string bookmakerId, CancellationToken cancellationToken = default)
        => await Set.AsNoTracking()
            .Where(s => s.BookmakerId == bookmakerId)
            .OrderBy(s => s.StartTime)
            .ToListAsync(cancellationToken);
}
