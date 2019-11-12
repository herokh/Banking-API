using Banking.Application.Models;
using System;

namespace Banking.Application.DTOs
{
    public class TransferMoneyFullDto : TransferMoneyDto
    {
        public Account sender { get; set; }
        public Account payee { get; set; }

        public double transfer_fee { get; set; }
        public DateTime transfer_date { get; set; }
    }
}
