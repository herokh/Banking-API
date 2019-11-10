using Banking.Application.DTOs;
using Banking.Application.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Banking.Application.Models
{
    public class Account : IEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("([NL]{2})(\\d{2})([A-Z]{4})(\\d{10})")]
        public string IBanNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; }

        public IEnumerable<Statement> Statements { get; set; }
    }
}
