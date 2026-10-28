using Casino.Domain.Enums;
using Casino.Domain.Interfaces;

namespace Casino.Domain.Entities;

public class WalletTransaction : IEntity
{
    public int Id { get; set; }
    public int WalletId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionType TransactionType { get; set; }

    public Wallet Wallet { get; set; } = null!;
}
