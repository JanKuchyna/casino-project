using Casino.Domain.Entities;
using Casino.Domain.Enums;
using Casino.Domain.Interfaces;
using Casino.Infrastructure.Data.Lookups;
using Casino.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Casino.Infrastructure.Data;

public class CasinoDbContext : IdentityDbContext<ApplicationUser>, IUnitOfWork
{
    public CasinoDbContext(DbContextOptions<CasinoDbContext> options) : base(options)
    {
    }

    public DbSet<Wallet> Wallets => Set<Wallet>();
    public DbSet<WalletTransaction> WalletTransactions => Set<WalletTransaction>();
    public DbSet<SportEvent> SportEvents => Set<SportEvent>();
    public DbSet<Odd> Odds => Set<Odd>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<TicketItem> TicketItems => Set<TicketItem>();
    public DbSet<CasinoGame> CasinoGames => Set<CasinoGame>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(e =>
        {
            e.Property(u => u.FirstName).HasMaxLength(100);
            e.Property(u => u.LastName).HasMaxLength(100);
        });

        builder.Entity<Wallet>(e =>
        {
            e.HasOne<ApplicationUser>()
                .WithOne(u => u.Wallet)
                .HasForeignKey<Wallet>(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            e.Property(w => w.Balance).HasPrecision(18, 2);
        });

        builder.Entity<WalletTransaction>(e =>
        {
            e.HasOne(t => t.Wallet)
                .WithMany(w => w.Transactions)
                .HasForeignKey(t => t.WalletId)
                .OnDelete(DeleteBehavior.Cascade);
            e.Property(t => t.Amount).HasPrecision(18, 2);
        });

        builder.Entity<SportEvent>(e =>
        {
            e.HasOne<ApplicationUser>()
                .WithMany(u => u.ManagedSportEvents)
                .HasForeignKey(s => s.BookmakerId)
                .OnDelete(DeleteBehavior.Restrict);
            e.Property(s => s.Name).HasMaxLength(200);
        });

        builder.Entity<Odd>(e =>
        {
            e.HasOne(o => o.SportEvent)
                .WithMany(s => s.Odds)
                .HasForeignKey(o => o.SportEventId)
                .OnDelete(DeleteBehavior.Cascade);
            e.Property(o => o.Value).HasPrecision(18, 2);
        });

        builder.Entity<Ticket>(e =>
        {
            e.HasOne<ApplicationUser>()
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne<ApplicationUser>()
                .WithMany(u => u.ManagedTickets)
                .HasForeignKey(t => t.BookmakerId)
                .OnDelete(DeleteBehavior.Restrict);
            e.Property(t => t.Stake).HasPrecision(18, 2);
        });

        builder.Entity<TicketItem>(e =>
        {
            e.HasOne(i => i.Ticket)
                .WithMany(t => t.Items)
                .HasForeignKey(i => i.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(i => i.Odd)
                .WithMany(o => o.TicketItems)
                .HasForeignKey(i => i.OddId)
                .OnDelete(DeleteBehavior.Restrict);
            e.Property(i => i.OddSnapshot).HasPrecision(18, 2);
        });

        builder.Entity<CasinoGame>(e =>
        {
            e.Property(g => g.Name).HasMaxLength(100);
            e.Property(g => g.Description).HasMaxLength(1000);
            e.Property(g => g.WinRate).HasPrecision(5, 2);
        });

        ConfigureEnumLookup<UserRole>(builder, "UserRoles");
        ConfigureEnumLookup<BetType>(builder, "BetTypes");
        ConfigureEnumLookup<EventResult>(builder, "EventResults");
        ConfigureEnumLookup<TicketStatus>(builder, "TicketStatuses");
        ConfigureEnumLookup<TransactionType>(builder, "TransactionTypes");

        builder.Entity<ApplicationUser>()
            .HasOne<EnumLookup<UserRole>>().WithMany()
            .HasForeignKey(u => u.Role)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Odd>()
            .HasOne<EnumLookup<BetType>>().WithMany()
            .HasForeignKey(o => o.BetType)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<SportEvent>()
            .HasOne<EnumLookup<EventResult>>().WithMany()
            .HasForeignKey(s => s.Result)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Ticket>()
            .HasOne<EnumLookup<TicketStatus>>().WithMany()
            .HasForeignKey(t => t.Status)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<WalletTransaction>()
            .HasOne<EnumLookup<TransactionType>>().WithMany()
            .HasForeignKey(t => t.TransactionType)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureEnumLookup<TEnum>(ModelBuilder builder, string tableName)
        where TEnum : struct, Enum
    {
        builder.Entity<EnumLookup<TEnum>>(e =>
        {
            e.ToTable(tableName);
            e.HasKey(l => l.Id);
            e.Property(l => l.Id).ValueGeneratedNever();
            e.Property(l => l.Name).HasMaxLength(50);
            e.HasData(Enum.GetValues<TEnum>()
                .Select(v => new EnumLookup<TEnum> { Id = v, Name = v.ToString() }));
        });
    }
}
