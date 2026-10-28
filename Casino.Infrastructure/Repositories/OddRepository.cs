using Casino.Domain.Entities;
using Casino.Domain.Interfaces;
using Casino.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Casino.Infrastructure.Repositories;

public class OddRepository : Repository<Odd>, IOddRepository
{
    public OddRepository(CasinoDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Odd>> GetBySportEventAsync(int sportEventId, CancellationToken cancellationToken = default)
        => await Set.AsNoTracking()
            .Where(o => o.SportEventId == sportEventId)
            .ToListAsync(cancellationToken);
}
