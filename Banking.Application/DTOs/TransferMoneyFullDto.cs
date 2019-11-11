using Banking.Application.Models;

namespace Banking.Application.DTOs
{
    public class TransferMoneyFullDto : TransferMoneyDto
    {
        public Account sender { get; set; }
        public Account payee { get; set; }
    }
}
