using Banking.Application.DTOs;
using Banking.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Banking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatementsController : ControllerBase
    {
        private readonly IStatementService _statementService;
        public StatementsController(IStatementService statementService)
        {
            _statementService = statementService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatementDto>> Get(int id)
        {
            var viewDto = await _statementService.Get(id);
            return Ok(viewDto);
        }

        [HttpGet("{ibanNumber}/account")]
        public async Task<ActionResult<IEnumerable<StatementDto>>> GetAll(string ibanNumber)
        {
            var viewDto = await _statementService.GetAll(ibanNumber);
            return Ok(viewDto);
        }

        [HttpPost("deposit")]
        public async Task<ActionResult<StatementDto>> Post(StatementDepositDto dto)
        {
            var viewDto = await _statementService.Deposit(dto);
            return CreatedAtAction("Get", new { viewDto.id }, viewDto);
        }
    }
}
