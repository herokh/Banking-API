using Banking.Application.DTOs;
using Banking.Application.Enums;
using Banking.Application.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Banking.Application.Models
{
    public class Statement : IEntity
    {
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public double Fee { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreateAt { get; set; }

        [Required]
        public StatementType StatementType { get; set; }

        [Required]
        public Account Account { get; set; }
        
    }
}
