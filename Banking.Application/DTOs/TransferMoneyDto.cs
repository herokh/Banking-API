namespace Banking.Application.DTOs
{
    public class TransferMoneyDto
    {
        public string sender_iban_number { get; set; }
        public string payee_iban_number { get; set; }
        public decimal amount { get; set; }

    }
}
