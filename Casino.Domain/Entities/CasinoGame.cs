using Casino.Domain.Interfaces;

namespace Casino.Domain.Entities;

public class CasinoGame : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Pravděpodobnost výhry v procentech (0–100)
    public decimal WinRate { get; set; }
}
