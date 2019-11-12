using Banking.Application.Models;
using Banking.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Banking.Infrastructure.Repositories.EFCore
{
    public class AccountRepository : EfCoreRepository<Account, BankingContext>, IAccountRepository
    {
        private readonly BankingContext _context;
        public AccountRepository(BankingContext context) : base(context)
        {
            _context = context;
        }

        public AccountRepository() : base(null)
        {
            // unit test
        }

        public virtual async Task<Account> GetByIBanNumber(string ibanNumber)
        {
            Account account = await _context.Account.FirstOrDefaultAsync(x => x.IBanNumber == ibanNumber);

            return account;
        }

        public virtual async Task<bool> IsUniqueIbanNumber(string ibanNumber)
        {
            Account account = await GetByIBanNumber(ibanNumber);

            return account == null;
        }

    }
}
