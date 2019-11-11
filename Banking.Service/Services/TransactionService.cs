using Banking.Application.DTOs;
using Banking.Application.Exceptions;
using Banking.Application.Helpers;
using Banking.Application.Models;
using Banking.Infrastructure.Repositories.EFCore;
using Banking.Service.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Banking.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AccountRepository _accountRepository;
        private readonly StatementRepository _statementRepository;
        private readonly TransactionRepository _transactionRepository;
        public TransactionService(
            AccountRepository accountRepository,
            StatementRepository statementRepository,
            TransactionRepository transactionRepository
            )
        {
            _accountRepository = accountRepository;
            _statementRepository = statementRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<TransferMoneyResultDto> TransferMoney(TransferMoneyDto dto)
        {
            Account sender = await _accountRepository.GetByIBanNumber(dto.sender_iban_number);
            if (sender == null)
                throw new AccountNotFoundException($"Source Iban number[{dto.sender_iban_number}] was not found");

            Account payee = await _accountRepository.GetByIBanNumber(dto.payee_iban_number);
            if (payee == null)
                throw new AccountNotFoundException($"Destination Iban number[{dto.payee_iban_number}] was not found");


            var statements = await _statementRepository.GetByAccountId(sender.Id);
            var totalBalance = statements.Sum(x => StatementHelper.CalculateActualAmount(x.Amount, x.Fee));
            if (totalBalance < dto.amount)
                throw new MoneyNotEnoughException("you have not enough money");

            TransferMoneyFullDto fullDto = new TransferMoneyFullDto
            {
                amount = dto.amount,
                sender = sender,
                payee = payee
            };

            var dtoResult = await _transactionRepository.TransferMoney(fullDto);

            return dtoResult;
        }
    }
}
