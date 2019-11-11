using Banking.Application.DTOs;
using FluentValidation;

namespace Banking.Application.Validations
{
    public class TransactionTransferMoneyValidator : AbstractValidator<TransferMoneyDto>
    {
        public TransactionTransferMoneyValidator()
        {
            RuleFor(x => x.sender_iban_number).NotEmpty().WithMessage("Please input source iban number");
            RuleFor(x => x.payee_iban_number).NotEmpty().WithMessage("Please input destination iban number");
            RuleFor(x => x.amount).NotEmpty().WithMessage("Please input amount of cash");
        }

    }
}
