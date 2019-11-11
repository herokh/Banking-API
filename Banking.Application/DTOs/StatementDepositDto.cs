using Banking.Application.Enums;

namespace Banking.Application.DTOs
{
    public class StatementDepositDto
    {
        public decimal amount { get; set; }
        public string iban_number { get; set; }
    }
}
