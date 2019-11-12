using Banking.Application.DTOs;
using Banking.Application.Models;
using Banking.Service.Services;
using Banking.Service.Validations;
using FluentValidation;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Banking.Test.Services
{
    public class AccountServiceTest : TestBase
    {
        private AccountService _testService;

        [SetUp]
        public void Setup()
        {
            _testService = new AccountService(new AccountRegisterValidator(MockAccountRepository.Object),
                MockAccountRepository.Object);
        }

        [TestCase("")]
        [TestCase("hello")]
        public void Register_Should_ThrowValidationException_When_IbanNumberIsInvalid(string ibanNumber)
        {
            var dto = new AccountRegisterDto
            {
                iban_number = ibanNumber,
                acccount_name = "My name is Hero"
            };

            Assert.ThrowsAsync<ValidationException>(() => _testService.Register(dto));
        }

        [TestCase]
        public void Register_Should_ThrowValidationException_When_IbanNumberAlreadyExists()
        {
            var dto = new AccountRegisterDto
            {
                iban_number = "NL13ABNA7035378898",
                acccount_name = "My name is Hero"
            };

            MockAccountRepository.Setup(x => x.IsUniqueIbanNumber(It.IsAny<string>())).Returns(Task.FromResult(false));

            Assert.ThrowsAsync<ValidationException>(() => _testService.Register(dto));
        }

        [TestCase("")]
        [TestCase("rabbit")]
        public void Register_Should_ThrowValidationException_When_AccontNameIsInvalid(string accountName)
        {
            var dto = new AccountRegisterDto
            {
                iban_number = "NL13ABNA7035378898",
                acccount_name = accountName
            };

            MockAccountRepository.Setup(x => x.IsUniqueIbanNumber(It.IsAny<string>())).Returns(Task.FromResult(true));

            Assert.ThrowsAsync<ValidationException>(() => _testService.Register(dto));
        }

        [TestCase]
        public void Register_Should_Success_When_DtoIsValid()
        {
            var dto = new AccountRegisterDto
            {
                iban_number = "NL13ABNA7035378898",
                acccount_name = "My name is Hero"
            };

            MockAccountRepository.Setup(x => x.IsUniqueIbanNumber(It.IsAny<string>())).Returns(Task.FromResult(true));
            MockAccountRepository.Setup(x => x.Add(It.IsAny<Account>())).Returns(Task.FromResult(new Account()));

            var result = _testService.Register(dto).Result;
            Assert.IsNotNull(result);
        }

    }
}
