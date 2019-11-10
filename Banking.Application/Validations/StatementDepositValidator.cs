using Banking.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Application.Validations
{
    public class StatementDepositValidator : AbstractValidator<StatementDepositDto>
    {
        public StatementDepositValidator()
        {
            RuleFor(x => x.iban_number)
                .NotEmpty()
                .Matches("([NL]{2})(\\d{2})([A-Z]{4})(\\d{10})")
                .WithMessage("Iban number is invalid");

            RuleFor(x => x.amount)
                .NotNull()
                .WithMessage("amount can not be null");

            RuleFor(x => x.statement_type)
                .NotNull()
                .WithMessage("statement type can not be null");
        }
    }
}
