using Banking.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure
{
    public class BankingContext : DbContext
    {
        public BankingContext (DbContextOptions<BankingContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<Statement> Statement { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Statement>().ToTable("Statement");

            modelBuilder.Entity<Statement>(entityBuilder =>
            {
                entityBuilder.Property(p => p.Amount).HasColumnType("DECIMAL(13, 4)");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
