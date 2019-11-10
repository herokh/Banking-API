using Banking.Application.DTOs;
using Banking.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Banking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashTransferController : ControllerBase
    {
        private readonly ICashTransferService _cashTransferService;
        public CashTransferController(ICashTransferService cashTransferService)
        {
            _cashTransferService = cashTransferService;
        }

        [HttpPost]
        public async Task<ActionResult<CashTransferResultDto>> Post(CashTransferDto dto)
        {
            var viewDto = await _cashTransferService.Transfer(dto);
            return Ok(viewDto);
        }

    }
}
