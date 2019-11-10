using Banking.Application.Models;
using Banking.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Infrastructure.Repositories.EFCore
{
    public class EfCoreAccountRepository : EfCoreRepository<Account, BankingContext>
    {
        public EfCoreAccountRepository(BankingContext context) : base(context)
        {
        }
    }
}
