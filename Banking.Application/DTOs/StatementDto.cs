using Banking.Application.Enums;
using Banking.Application.Helpers;
using System;

namespace Banking.Application.DTOs
{
    public class StatementDto
    {
        public int id { get; set; }
        public decimal raw_amount { private get; set; }
        public StatementType statement_type { private get; set; }
        public double fee_as_percent { private get; set; }
        public DateTime create_at { get; set; }
        public decimal amount {
        get
            {
                return StatementHelper.CalculateActualAmount(raw_amount, fee_as_percent);
            }
        }
        public string type
        {
            get
            {
                return statement_type.ToString();
            }
        }
    }
}
