using Banking.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Banking.Infrastructure.Repositories.Interfaces
{
    public interface IStatementRepository
    {
        Task<IEnumerable<Statement>> GetByAccountId(int accountId);
    }
}
