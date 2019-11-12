using Banking.Application.Models;
using Banking.Infrastructure.Repositories.EFCore;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace Banking.Test
{
    public abstract class TestBase
    {
        protected Mock<AccountRepository> MockAccountRepository { get; private set; }
        protected Mock<StatementRepository> MockStatementRepository { get; private set; }
        protected Mock<TransactionRepository> MockTransactionRepository { get; private set; }

        protected Mock<IOptions<AppSettings>> MockAppSettings { get; private set; }

        [SetUp]
        public void TestBaseSetup()
        {
            MockAccountRepository = new Mock<AccountRepository>();
            MockStatementRepository = new Mock<StatementRepository>();
            MockTransactionRepository = new Mock<TransactionRepository>();

            MockAppSettings = new Mock<IOptions<AppSettings>>();

            MockAppSettings.SetupGet(x => x.Value).Returns(new AppSettings
            {
                DepositFee = 0.1,
                TransferFee = 0
            });
        }

    }
}