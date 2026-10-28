using Casino.Domain.Entities;
using Casino.Domain.Interfaces;
using Casino.Infrastructure.Data;

namespace Casino.Infrastructure.Repositories;

public class CasinoGameRepository : Repository<CasinoGame>, ICasinoGameRepository
{
    public CasinoGameRepository(CasinoDbContext context) : base(context)
    {
    }
}
