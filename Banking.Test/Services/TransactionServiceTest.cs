using Banking.Application.DTOs;
using Banking.Application.Exceptions;
using Banking.Application.Models;
using Banking.Service.Services;
using Banking.Service.Validations;
using FluentValidation;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Test.Services
{
    public class TransactionServiceTest : TestBase
    {
        private TransactionService testService;

        [SetUp]
        public void Setup()
        {
            testService = new TransactionService(
                MockAccountRepository.Object,
                MockStatementRepository.Object,
                MockTransactionRepository.Object,
                new TransactionTransferMoneyValidator(),
                MockAppSettings.Object
            );
        }

        [TestCase]
        public void TransferMoney_Should_ThrowValidationException_When_AmountIsLessThanZero()
        {
            var dto = new TransferMoneyDto
            {
                payee_iban_number = "NL13ABNA7035378898",
                sender_iban_number  = "NL95INGB1938035321",
                amount = -1
            };

            Assert.ThrowsAsync<ValidationException>(() => testService.TransferMoney(dto));
        }

        [TestCase]
        public void TransferMoney_Should_ThrowValidationException_When_SenderIbanNumberIsInvalid()
        {
            var dto = new TransferMoneyDto
            {
                payee_iban_number = "NL13ABNA7035378898",
                sender_iban_number = "hello",
                amount = -1
            };

            Assert.ThrowsAsync<ValidationException>(() => testService.TransferMoney(dto));
        }

        [TestCase]
        public void TransferMoney_Should_ThrowValidationException_When_PayeeIbanNumberIsInvalid()
        {
            var dto = new TransferMoneyDto
            {
                payee_iban_number = "thank you",
                sender_iban_number = "NL13ABNA7035378898",
                amount = -1
            };

            Assert.ThrowsAsync<ValidationException>(() => testService.TransferMoney(dto));
        }

        [TestCase]
        public void TransferMoney_Should_ThrowAccountNotFoundExceptionn_When_SenderAccountWasNotFound()
        {
            var sender = "NL95INGB1938035321";
            var payee = "NL13ABNA7035378898";
            var dto = new TransferMoneyDto
            {
                payee_iban_number = payee,
                sender_iban_number = sender,
                amount = 2000
            };

            Account senderInstance = null;
            Account payeeInstance = new Account();
            MockAccountRepository.Setup(x => x.GetByIBanNumber(sender)).Returns(Task.FromResult(senderInstance));
            MockAccountRepository.Setup(x => x.GetByIBanNumber(payee)).Returns(Task.FromResult(payeeInstance));

            Assert.ThrowsAsync<AccountNotFoundException>(() => testService.TransferMoney(dto));
        }

        [TestCase]
        public void TransferMoney_Should_ThrowAccountNotFoundExceptionn_When_PayeeAccountWasNotFound()
        {
            var sender = "NL95INGB1938035321";
            var payee = "NL13ABNA7035378898";
            var dto = new TransferMoneyDto
            {
                payee_iban_number = payee,
                sender_iban_number = sender,
                amount = 2000
            };

            Account senderInstance = new Account();
            Account payeeInstance = null;
            MockAccountRepository.Setup(x => x.GetByIBanNumber(sender)).Returns(Task.FromResult(senderInstance));
            MockAccountRepository.Setup(x => x.GetByIBanNumber(payee)).Returns(Task.FromResult(payeeInstance));

            Assert.ThrowsAsync<AccountNotFoundException>(() => testService.TransferMoney(dto));
        }

        [TestCase]
        public void TransferMoney_Should_ThrowMoneyNotEnoughException_When_TotalBalanceIsLessThanAmoutToTransfer()
        {
            var sender = "NL95INGB1938035321";
            var payee = "NL13ABNA7035378898";
            var dto = new TransferMoneyDto
            {
                payee_iban_number = payee,
                sender_iban_number = sender,
                amount = 2000
            };

            Account senderInstance = new Account();
            Account payeeInstance = new Account();
            MockAccountRepository.Setup(x => x.GetByIBanNumber(sender)).Returns(Task.FromResult(senderInstance));
            MockAccountRepository.Setup(x => x.GetByIBanNumber(payee)).Returns(Task.FromResult(payeeInstance));

            IEnumerable<Statement> statements = new List<Statement>
            {
                new Statement { Amount = 100 },
                new Statement { Amount = 1000 }
            };
            MockStatementRepository.Setup(x => x.GetByAccountId(It.IsAny<int>())).Returns(Task.FromResult(statements));

            Assert.ThrowsAsync<MoneyNotEnoughException>(() => testService.TransferMoney(dto));
        }

        [TestCase]
        public void TransferMoney_Should_Success_When_AmountIsLessThanTotalBalance()
        {
            var sender = "NL95INGB1938035321";
            var payee = "NL13ABNA7035378898";
            var dto = new TransferMoneyDto
            {
                payee_iban_number = payee,
                sender_iban_number = sender,
                amount = 2000
            };

            Account senderInstance = new Account();
            Account payeeInstance = new Account();
            MockAccountRepository.Setup(x => x.GetByIBanNumber(sender)).Returns(Task.FromResult(senderInstance));
            MockAccountRepository.Setup(x => x.GetByIBanNumber(payee)).Returns(Task.FromResult(payeeInstance));

            IEnumerable<Statement> statements = new List<Statement>
            {
                new Statement { Amount = 100 },
                new Statement { Amount = 1000 },
                new Statement { Amount = 10000 }
            };
            MockStatementRepository.Setup(x => x.GetByAccountId(It.IsAny<int>())).Returns(Task.FromResult(statements));

            TransferMoneyResultDto resultDto = new TransferMoneyResultDto { success = true };
            MockTransactionRepository.Setup(x => x.SaveTransferringMoney(It.IsAny<TransferMoneyFullDto>())).Returns(Task.FromResult(resultDto));

            var result = testService.TransferMoney(dto).Result;
            Assert.IsTrue(result.success);
        }

    }
}
