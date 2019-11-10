using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Application.DTOs
{
    public class CashTransferDto
    {
        public string source_iban_number { get; set; }
        public string destination_iban_number { get; set; }
        public double amount { get; set; }

    }
}
