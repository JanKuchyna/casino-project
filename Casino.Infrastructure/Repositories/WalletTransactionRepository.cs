using Casino.Domain.Entities;
using Casino.Domain.Interfaces;
using Casino.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Casino.Infrastructure.Repositories;

public class WalletTransactionRepository : Repository<WalletTransaction>, IWalletTransactionRepository
{
    public WalletTransactionRepository(CasinoDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<WalletTransaction>> GetByWalletAsync(int walletId, CancellationToken cancellationToken = default)
        => await Set.AsNoTracking()
            .Where(t => t.WalletId == walletId)
            .OrderByDescending(t => t.Date)
            .ToListAsync(cancellationToken);
}
