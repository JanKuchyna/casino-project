using Casino.Domain.Enums;
using Casino.Domain.Interfaces;

namespace Casino.Domain.Entities;

public class SportEvent : IEntity
{
    public int Id { get; set; }
    public string BookmakerId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public bool IsActive { get; set; }

    public EventResult? Result { get; set; }

    public ICollection<Odd> Odds { get; set; } = new List<Odd>();
}
