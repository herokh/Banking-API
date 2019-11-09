using Banking.Application.Enums;
using System;

namespace Banking.Application.Models
{
    public class Statement
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime CreateAt { get; set; }
        public StatementType StatementType { get; set; }

        public Account Account { get; set; }
    }
}
