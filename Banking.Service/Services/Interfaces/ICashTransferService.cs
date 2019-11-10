using Banking.Application.DTOs;
using System.Threading.Tasks;

namespace Banking.Service.Services.Interfaces
{
    public interface ICashTransferService
    {
        Task<CashTransferResultDto> Transfer(CashTransferDto dto);
    }
}
