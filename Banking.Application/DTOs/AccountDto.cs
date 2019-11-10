using System;

namespace Banking.Application.DTOs
{
    public class AccountDto : AccountRegisterDto
    { 
        public int id { get; set; }
        public DateTime register_date { get; set; }
    }
}
