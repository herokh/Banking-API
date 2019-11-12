using Banking.Application.DTOs;
using Banking.Application.Exceptions;
using Banking.Application.Helpers;
using Banking.Application.Models;
using Banking.Infrastructure.Repositories.EFCore;
using Banking.Service.Services.Interfaces;
using Banking.Service.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banking.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountRegisterValidator _accountRegisterValidator;
        private readonly AccountRepository _accountRepository;

        public AccountService(
            AccountRegisterValidator accountRegisterValidator,
            AccountRepository accountRepository
            )
        {
            _accountRegisterValidator = accountRegisterValidator;
            _accountRepository = accountRepository;
        }
        public async Task<AccountDto> Register(AccountRegisterDto dto)
        {
            ValidationHelper.Validate(_accountRegisterValidator, dto);

            var entity = new Account
            {
                AccountName = dto.acccount_name,
                IBanNumber = dto.iban_number,
                RegisterDate = DateTime.Now,
            };
            await _accountRepository.Add(entity);

            return ConvertToDto(entity);
        }

        public async Task<AccountDto> Get(string ibanNumber)
        {
            var account = await _accountRepository.GetByIBanNumber(ibanNumber);
            if (account == null)
                throw new AccountNotFoundException($"account was not found");

            return ConvertToDto(account);
        }

        public async Task<IEnumerable<AccountDto>> GetAll()
        {
            var accounts = await _accountRepository.GetAll();
            return accounts.Select(x => ConvertToDto(x));
        }

        private AccountDto ConvertToDto(Account account)
        {
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
