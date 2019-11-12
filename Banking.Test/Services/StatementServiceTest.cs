using Banking.Application.DTOs;
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
    public class StatementServiceTest : TestBase
    {
        private StatementService _testService;

        [SetUp]
        public void Setup()
        {
            _testService = new StatementService(
                new StatementDepositValidator(MockAccountRepository.Object),
                MockAccountRepository.Object,
                MockStatementRepository.Object,
                MockAppSettings.Object);
        }

        [TestCase]
        public void Deposit_Should_ThrowValidationException_When_IbanNumberIsInvalid()
        {
            var dto = new StatementDepositDto
            {
                iban_number = "hello",
                amount = 100
            };

            Assert.ThrowsAsync<ValidationException>(() => _testService.Deposit(dto));
        }

        [TestCase]
        public void Deposit_Should_ThrowValidationException_When_IbanNumberAlreadyExists()
        {
            var dto = new StatementDepositDto
            {
                iban_number = "NL13ABNA7035378898",
                amount = 100
            };

            MockAccountRepository.Setup(x => x.HasAccount(It.IsAny<string>())).Returns(Task.FromResult(false));

            Assert.ThrowsAsync<ValidationException>(() => _testService.Deposit(dto));
        }

        [TestCase]
        public void Deposit_Should_Success_When_DtoIsValid()
        {
            var dto = new StatementDepositDto
            {
                iban_number = "NL13ABNA7035378898",
                amount = 100
            };

            MockAccountRepository.Setup(x => x.HasAccount(It.IsAny<string>())).Returns(Task.FromResult(true));
            MockStatementRepository.Setup(x => x.Add(It.IsAny<Statement>())).Returns(Task.FromResult(new Statement()));

            var result = _testService.Deposit(dto).Result;

            Assert.IsNotNull(result);
        }

    }
}
