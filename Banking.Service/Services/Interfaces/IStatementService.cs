using Banking.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Banking.Service.Services.Interfaces
{
    public interface IStatementService
    {
        Task<StatementDto> Deposit(StatementDepositDto dto);
        Task<StatementDto> Get(int id);
        Task<IEnumerable<StatementDto>> GetAll(string ibanNumber);
    }
}
