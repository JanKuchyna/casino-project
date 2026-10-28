using Casino.Domain.Entities;

namespace Casino.Domain.Interfaces;

public interface IWalletTransactionRepository : IRepository<WalletTransaction>
{
    Task<IReadOnlyList<WalletTransaction>> GetByWalletAsync(int walletId, CancellationToken cancellationToken = default);
}
