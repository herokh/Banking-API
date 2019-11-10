using Banking.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Banking.Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDto> Create(AccountRegisterDto dto);
        Task<AccountDto> Get(string ibanNumber);
        Task<IEnumerable<AccountDto>> GetAll();
    }
}
