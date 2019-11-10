using Banking.Application.DTOs;
using Banking.Application.Helpers;
using Banking.Application.Models;
using Banking.Application.Validations;
using Banking.Infrastructure.Repositories.EFCore;
using Banking.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banking.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountRegisterValidator _validator;
        private readonly AccountRepository _repository;

        public AccountService(
            AccountRegisterValidator validator,
            AccountRepository repository
            )
        {
            _validator = validator;
            _repository = repository;
        }
        public async Task<AccountDto> Create(AccountRegisterDto dto)
        {
            ValidationHelper.Validate(_validator, dto);
            var entity = new Account
            {
                AccountName = dto.acccount_name,
                IBanNumber = dto.iban_number,
                RegisterDate = DateTime.Now,
            };
            await _repository.Add(entity);

            return ConvertToDto(entity);
        }

        public async Task<AccountDto> Get(string ibanNumber)
        {
            var entity = await _repository.GetByIBanNumber(ibanNumber);

            return ConvertToDto(entity);
        }

        public async Task<IEnumerable<AccountDto>> GetAll()
        {
            var accounts = await _repository.GetAll();
            return accounts.Select(x => ConvertToDto(x));
        }

        private AccountDto ConvertToDto(Account account)
        {
            if (account == null)
                return null;

            return new AccountDto
            {
                id = account.Id,
                acccount_name = account.AccountName,
                iban_number = account.IBanNumber,
                register_date = account.RegisterDate
            };
        }
    }
}
