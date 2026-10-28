using Casino.Domain.Entities;
using Casino.Domain.Interfaces;
using Casino.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Casino.Infrastructure.Repositories;

public class WalletRepository : Repository<Wallet>, IWalletRepository
{
    public WalletRepository(CasinoDbContext context) : base(context)
    {
    }

    public async Task<Wallet?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        => await Set.FirstOrDefaultAsync(w => w.UserId == userId, cancellationToken);
}
