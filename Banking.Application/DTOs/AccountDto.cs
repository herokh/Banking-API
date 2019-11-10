using System;

namespace Banking.Application.DTOs
{
    public class AccountDto
    { 
        public int id { get; set; }
        public string iban_number { get; set; }
        public string acccount_name { get; set; }
        public DateTime register_date { get; set; }
    }
}
