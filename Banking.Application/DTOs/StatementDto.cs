using Banking.Application.Enums;
using System;

namespace Banking.Application.DTOs
{
    public class StatementDto : StatementDepositDto
    {
        public int id { get; set; }
        public double fee_as_percent { get; set; }
        public DateTime create_at { get; set; }
        public double actual_amount {
        get
            {
                var amountOfFee = (amount * fee_as_percent) / 100;
                return amount - amountOfFee;
            }
        }
    }
}
