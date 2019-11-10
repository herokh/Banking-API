using Banking.Application.Enums;

namespace Banking.Application.DTOs
{
    public class StatementDepositDto
    {
        public double amount { get; set; }
        public StatementType statement_type { get; set; }
        public string iban_number { get; set; }
    }
}
