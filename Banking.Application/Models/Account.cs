using System;
using System.Collections.Generic;

namespace Banking.Application.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string IBanNumber { get; set; }
        public string AccountName { get; set; }
        public string AccountBranch { get; set; }
        public double AvailableBalance { get; set; }
        public DateTime RegisterDate { get; set; }

        public IEnumerable<Statement> Statements { get; set; }
    }
}
