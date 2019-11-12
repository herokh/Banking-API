using Banking.Application.DTOs;
using Banking.Application.Enums;
using Banking.Application.Exceptions;
using Banking.Application.Models;
using Banking.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Banking.Infrastructure.Repositories.EFCore
{
    public class TransactionRepository : EfCoreRepository<Transaction, BankingContext>, ITransactionRepository
    {
        private readonly BankingContext _context;
        private readonly StatementRepository _statementRepository;

        public TransactionRepository(BankingContext context, StatementRepository statementRepository) : base(context)
        {
            _context = context;
            _statementRepository = statementRepository;
        }

        public async Task<TransferMoneyResultDto> SaveTransferringMoney(TransferMoneyFullDto dto)
        {
            TransferMoneyResultDto resultDto = new TransferMoneyResultDto();

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Statement senderStatement = await _statementRepository.Add(new Statement
                    {
                        Account = dto.sender,
                        StatementType = StatementType.Transfer,
                        Fee = dto.transfer_fee,
                        Amount = dto.amount,
                        CreateAt = dto.transfer_date
                    });

                    Statement payeeStatement = await _statementRepository.Add(new Statement
                    {
                        Account = dto.payee,
                        StatementType = StatementType.Deposit,
                        Fee = dto.transfer_fee,
                        Amount = dto.amount,
                        CreateAt = dto.transfer_date
                    });

                    Transaction trasactionResult = await Add(new Transaction
                    {
                        PayeeStatement = payeeStatement,
                        SenderStatement = senderStatement,
                        TransferAt = dto.transfer_date
                    });

                    await transaction.CommitAsync();
                    resultDto.success = true;
                }
                catch (Exception ex)
                {
                    throw new TransactionFailureException("transaction error", ex);
                }
            }

            return resultDto;
        }

    }
}
