namespace Casino.Domain.Interfaces;

// Uloží změny ze všech repozitářů najednou (např. tiket + stržení z peněženky v jedné transakci)
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
