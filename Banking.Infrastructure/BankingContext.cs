using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
