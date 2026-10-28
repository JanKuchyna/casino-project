using Casino.Domain.Entities;

namespace Casino.Domain.Interfaces;

public interface ISportEventRepository : IRepository<SportEvent>
{
    Task<SportEvent?> GetWithOddsAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SportEvent>> GetActiveAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SportEvent>> GetByBookmakerAsync(string bookmakerId, CancellationToken cancellationToken = default);
}
