using Casino.Domain.Interfaces;

namespace Casino.Domain.Entities;

public class Wallet : IEntity
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public decimal Balance { get; set; }

    public ApplicationUser User { get; set; } = null!;
    public ICollection<WalletTransaction> Transactions { get; set; } = new List<WalletTransaction>();
}
