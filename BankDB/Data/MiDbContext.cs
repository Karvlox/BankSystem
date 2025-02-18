using Microsoft.EntityFrameworkCore;
using BankDB.Models;

namespace BankDB.Data;

public class MiDbContext : DbContext
{
    public MiDbContext(DbContextOptions<MiDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<UserAccess> UserAccesses { get; set; }
    public DbSet<Role> Roles { get; set; }    
    public DbSet<TypeTransaction> TypeTransactions { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relación User-Account (uno a muchos)
        modelBuilder.Entity<Account>()
            .HasOne(a => a.User)
            .WithMany(u => u.Accounts)
            .HasForeignKey(a => a.UserID)
            .OnDelete(DeleteBehavior.Cascade);

        // Relación User-UserAccess (uno a muchos)
        modelBuilder.Entity<UserAccess>()
            .HasOne(ua => ua.User)
            .WithMany(u => u.UserAccesses)
            .HasForeignKey(ua => ua.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relación UserAccess-Role (muchos a uno)
        modelBuilder.Entity<UserAccess>()
            .HasOne(ua => ua.Role)
            .WithMany(r => r.UserAccesses)
            .HasForeignKey(ua => ua.RoleId);

        // Relación Transaction-Sender Account (muchos a uno)
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Sender)
            .WithMany(a => a.Transactions)
            .HasForeignKey(t => t.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relación Transaction-Receiver Account (muchos a uno)
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Receiver)
            .WithMany()
            .HasForeignKey(t => t.RecivedId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relación Transaction-TypeTransaction (muchos a uno)
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.TypeTransaction)
            .WithMany(tt => tt.Transactions)
            .HasForeignKey(t => t.TypeTransactionId);
    }

}