using Casino.Domain.Entities;
using Casino.Domain.Interfaces;
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
        // Identity tabulky (AspNetUsers, AspNetRoles, ...) musí být nakonfigurovány jako první
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(e =>
        {
            e.Property(u => u.FirstName).HasMaxLength(100);
            e.Property(u => u.LastName).HasMaxLength(100);
        });

        builder.Entity<Wallet>(e =>
        {
            // 1:1 – EF vytvoří na UserId unikátní index
            e.HasOne(w => w.User)
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
            e.HasOne(s => s.Bookmaker)
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
            // Dvě vazby na stejnou tabulku uživatelů – sázkař a bookmaker
            e.HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(t => t.Bookmaker)
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
            // Kurz, na který už někdo vsadil, nejde smazat
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
    }
}
