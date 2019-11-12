using Banking.Application.DTOs;
using System.Threading.Tasks;

namespace Banking.Infrastructure.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<TransferMoneyResultDto> SaveTransferringMoney(TransferMoneyFullDto dto);
    }
}
