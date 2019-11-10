using Banking.Application.DTOs;
using Banking.Application.Exceptions;
using Banking.Application.Models;
using Banking.Infrastructure.Repositories.EFCore;
using Banking.Service.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Banking.Service.Services
{
    public class CashTransferService : ICashTransferService
    {
        private readonly AccountRepository _accountRepository;
        private readonly StatementRepository _statementRepository;
        public CashTransferService(
            AccountRepository accountRepository,
            StatementRepository statementRepository
            )
        {
            _accountRepository = accountRepository;
            _statementRepository = statementRepository;
        }

        public async Task<CashTransferResultDto> Transfer(CashTransferDto dto)
        {
            var yourAccount = await _accountRepository.GetByIBanNumber(dto.source_iban_number);
            if (yourAccount == null)
                throw new AccountNotFoundException($"Source Iban number[{dto.source_iban_number}] was not found");

            var destinationAccount = await _accountRepository.GetByIBanNumber(dto.destination_iban_number);
            if (destinationAccount == null)
                throw new AccountNotFoundException($"Destination Iban number[{dto.destination_iban_number}] was not found");

            await _statementRepository.Add(new Statement
            {
                Account = yourAccount,
                StatementType = Application.Enums.StatementType.Withdraw,
                Fee = 0,
                Amount = dto.amount,
                CreateAt = DateTime.Now
            });

            await _statementRepository.Add(new Statement
            {
                Account = destinationAccount,
                StatementType = Application.Enums.StatementType.Deposit,
                Fee = 0,
                Amount = dto.amount,
                CreateAt = DateTime.Now
            });

            return null;
        }
    }
}
