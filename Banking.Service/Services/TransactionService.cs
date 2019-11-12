using Banking.Application.DTOs;
using Banking.Application.Exceptions;
using Banking.Application.Helpers;
using Banking.Application.Models;
using Banking.Infrastructure.Repositories.EFCore;
using Banking.Service.Services.Interfaces;
using Banking.Service.Validations;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Banking.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AccountRepository _accountRepository;
        private readonly StatementRepository _statementRepository;
        private readonly TransactionRepository _transactionRepository;
        private readonly TransactionTransferMoneyValidator _transferMoneyValidator;
        private readonly AppSettings _appSettings;
        public TransactionService(
            AccountRepository accountRepository,
            StatementRepository statementRepository,
            TransactionRepository transactionRepository,
            TransactionTransferMoneyValidator transferMoneyValidator,
            IOptions<AppSettings> appSettings
            )
        {
            _accountRepository = accountRepository;
            _statementRepository = statementRepository;
            _transactionRepository = transactionRepository;
            _transferMoneyValidator = transferMoneyValidator;
            _appSettings = appSettings.Value;
        }

        public async Task<TransferMoneyResultDto> TransferMoney(TransferMoneyDto dto)
        {
            ValidationHelper.Validate(_transferMoneyValidator, dto);
            Account sender = await _accountRepository.GetByIBanNumber(dto.sender_iban_number);
            if (sender == null)
                throw new AccountNotFoundException("sender was not found");

            Account payee = await _accountRepository.GetByIBanNumber(dto.payee_iban_number);
            if (payee == null)
                throw new AccountNotFoundException("payee was not found");

            var statements = await _statementRepository.GetByAccountId(sender.Id);
            var totalBalance = statements.Sum(x => StatementHelper.CalculateActualAmount(x.Amount, x.Fee));
            if (totalBalance < dto.amount)
                throw new MoneyNotEnoughException("not enough money to transfer");

            TransferMoneyFullDto fullDto = new TransferMoneyFullDto
            {
                amount = dto.amount,
                sender = sender,
                payee = payee,
                transfer_date = DateTime.Now,
                transfer_fee = _appSettings.TransferFee
            };

            var dtoResult = await _transactionRepository.SaveTransferringMoney(fullDto);

            return dtoResult;
        }
    }
}
