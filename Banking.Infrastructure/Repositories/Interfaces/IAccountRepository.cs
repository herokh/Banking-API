using Banking.Application.Models;
using System.Threading.Tasks;

namespace Banking.Infrastructure.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetByIBanNumber(string ibanNumber);
        Task<bool> IsUniqueIbanNumber(string ibanNumber);
        Task<bool> HasAccount(string ibanNumber);
    }
}
