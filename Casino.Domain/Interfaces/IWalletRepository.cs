using Casino.Domain.Entities;

namespace Casino.Domain.Interfaces;

public interface IWalletRepository : IRepository<Wallet>
{
    Task<Wallet?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
}
