using Banking.Application.DTOs;
using FluentValidation;

namespace Banking.Application.Validations
{
    public class CashTransferValidator : AbstractValidator<CashTransferDto>
    {
        public CashTransferValidator()
        {
            RuleFor(x => x.source_iban_number).NotEmpty().WithMessage("Please input source iban number");
            RuleFor(x => x.destination_iban_number).NotEmpty().WithMessage("Please input destination iban number");
            RuleFor(x => x.amount).NotEmpty().WithMessage("Please input amount of cash");
        }

    }
}
