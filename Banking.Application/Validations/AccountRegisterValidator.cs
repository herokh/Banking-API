using Banking.Application.DTOs;
using FluentValidation;

namespace Banking.Application.Validations
{
    public class AccountRegisterValidator : AbstractValidator<AccountRegisterDto>
    {
        public AccountRegisterValidator()
        {
            RuleFor(x => x.iban_number)
                .NotEmpty()
                .Matches("([NL]{2})(\\d{2})([A-Z]{4})(\\d{10})")
                .WithMessage("Iban number is invalid");

            RuleFor(x => x.acccount_name)
                .NotEmpty()
                .WithMessage("Account name is empty");

        }

    }
}
