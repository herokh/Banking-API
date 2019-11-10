using Banking.Application.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banking.Infrastructure.Repositories.EFCore
{
    public class StatementRepository : EfCoreRepository<Statement, BankingContext>
    {
        private readonly BankingContext _context;
        public StatementRepository(BankingContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Statement>> GetByAccountId(int accountId)
        {
            return await _context.Statement.Where(x => x.Account != null && x.Account.Id == accountId).ToListAsync();
        }
    }
}
