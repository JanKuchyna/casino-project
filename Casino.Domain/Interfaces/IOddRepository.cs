using Casino.Domain.Entities;

namespace Casino.Domain.Interfaces;

public interface IOddRepository : IRepository<Odd>
{
    Task<IReadOnlyList<Odd>> GetBySportEventAsync(int sportEventId, CancellationToken cancellationToken = default);
}
