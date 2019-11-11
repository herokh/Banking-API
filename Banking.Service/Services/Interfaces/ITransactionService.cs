using Banking.Application.DTOs;
using System.Threading.Tasks;

namespace Banking.Service.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransferMoneyResultDto> TransferMoney(TransferMoneyDto dto);
    }
}
