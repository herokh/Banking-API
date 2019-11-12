using Banking.Application.DTOs;
using Banking.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Banking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAll()
        {
            var viewDto = await _accountService.GetAll();
            return Ok(viewDto);
        }

        [HttpGet("{ibanNumber}")]
        public async Task<ActionResult<AccountDto>> Get(string ibanNumber)
        {
            var viewDto = await _accountService.Get(ibanNumber);
            return Ok(viewDto);
        }

        [HttpPost]
        public async Task<ActionResult<AccountDto>> Post(AccountRegisterDto dto)
        {
            var viewDto = await _accountService.Register(dto);
            return CreatedAtAction("Get", new { ibanNumber = viewDto.iban_number }, viewDto);
        }

    }
}
