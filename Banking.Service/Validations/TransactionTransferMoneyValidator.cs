using Banking.Application.DTOs;
using FluentValidation;

namespace Banking.Service.Validations
{
    public class TransactionTransferMoneyValidator : AbstractValidator<TransferMoneyDto>
    {
        public TransactionTransferMoneyValidator()
        {
            RuleFor(x => x.sender_iban_number)
                .NotEmpty()
                .Matches("([NL]{2})(\\d{2})([A-Z]{4})(\\d{10})")
                .WithMessage("sender iban number is invalid");
            RuleFor(x => x.payee_iban_number)
                .NotEmpty()
                .Matches("([NL]{2})(\\d{2})([A-Z]{4})(\\d{10})")
                .WithMessage("payee iban number is invalid");
            RuleFor(x => x.amount)
                .NotEmpty().WithMessage("amount can not be empty")
                .GreaterThan(0).WithMessage("amount must more than 0");
        }

    }
}
