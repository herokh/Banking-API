using Banking.Application.Models;
using Banking.Infrastructure.Repositories.EFCore;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : EfCoreApiController<Account, EfCoreAccountRepository>
    {
        public AccountsController(EfCoreAccountRepository repository) : base(repository)
        {
        }

    }
}
