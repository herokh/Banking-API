using Banking.Application.DTOs;
using Banking.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Banking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IStatementService _statementService;
        public TransactionsController(
            ITransactionService transactionService,
            IStatementService statementService)
        {
            _transactionService = transactionService;
            _statementService = statementService;
        }

        [HttpPost("deposit")]
        public async Task<ActionResult<StatementDto>> Post(StatementDepositDto dto)
        {
            var viewDto = await _statementService.Deposit(dto);
            return Ok(viewDto);
        }

        [HttpPost("transfer")]
        public async Task<ActionResult<TransferMoneyResultDto>> Post(TransferMoneyDto dto)
        {
            var viewDto = await _transactionService.TransferMoney(dto);
            return Ok(viewDto);
        }

    }
}
