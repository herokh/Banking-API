using Banking.Application.DTOs;
using Banking.Application.Enums;
using Banking.Application.Exceptions;
using Banking.Application.Models;
using System;
using System.Threading.Tasks;

namespace Banking.Infrastructure.Repositories.EFCore
{
    public class TransactionRepository : EfCoreRepository<Transaction, BankingContext>
    {
        private readonly BankingContext _context;
        private readonly StatementRepository _statementRepository;

        public TransactionRepository(BankingContext context, StatementRepository statementRepository) : base(context)
        {
            _context = context;
            _statementRepository = statementRepository;
        }

        public async Task<TransferMoneyResultDto> TransferMoney(TransferMoneyFullDto dto)
        {
            TransferMoneyResultDto resultDto = new TransferMoneyResultDto();
            DateTime transferAt = DateTime.Now;
            // free fee
            double fee = 0;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Statement senderStatement = await _statementRepository.Add(new Statement
                    {
                        Account = dto.sender,
                        StatementType = StatementType.Transfer,
                        Fee = fee,
                        Amount = dto.amount,
                        CreateAt = transferAt
                    });

                    Statement payeeStatement = await _statementRepository.Add(new Statement
                    {
                        Account = dto.payee,
                        StatementType = StatementType.Deposit,
                        Fee = fee,
                        Amount = dto.amount,
                        CreateAt = transferAt
                    });

                    Transaction trasactionResult = await Add(new Transaction
                    {
                        PayeeStatement = payeeStatement,
                        SenderStatement = senderStatement,
                        TransferAt = transferAt
                    });

                    await transaction.CommitAsync();
                    resultDto.success = true;
                }
                catch (Exception ex)
                {
                    throw new TransactionFailureException("System error :)", ex);
                }
            }

            return resultDto;
        }

    }
}
