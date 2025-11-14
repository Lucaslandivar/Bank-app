using Microsoft.EntityFrameworkCore;
using _7Bank.Api.Models;

namespace _7Bank.Api.Data
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }
            public DbSet<Users> Users {  get; set; }
            public DbSet<Account> Accounts { get; set; }
            
            public DbSet<Transaction> Transactions {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(a => a.Balance)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(18, 2);
        }
    }


}
