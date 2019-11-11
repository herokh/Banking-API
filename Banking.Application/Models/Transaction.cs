using Banking.Application.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Banking.Application.Models
{
    public class Transaction : IEntity
    {
        public int Id { get; set; }

        [Required]
        public Statement SenderStatement { get; set; }

        [Required]
        public Statement PayeeStatement { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TransferAt { get; set; }

    }
}
