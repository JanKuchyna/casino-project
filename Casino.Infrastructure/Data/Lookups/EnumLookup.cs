namespace Casino.Infrastructure.Data.Lookups;

public class EnumLookup<TEnum> where TEnum : struct, Enum
{
    public TEnum Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
