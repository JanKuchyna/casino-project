using Casino.Domain.Interfaces;
using Casino.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Casino.Infrastructure.Repositories;

// Změny se neukládají zde, ale přes IUnitOfWork.SaveChangesAsync
public class Repository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly CasinoDbContext Context;
    protected readonly DbSet<T> Set;

    public Repository(CasinoDbContext context)
    {
        Context = context;
        Set = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await Set.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await Set.AsNoTracking().ToListAsync(cancellationToken);

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        => await Set.AddAsync(entity, cancellationToken);

    public void Update(T entity) => Set.Update(entity);

    public void Delete(T entity) => Set.Remove(entity);
}
