using Banking.Application.DTOs;
using Banking.Infrastructure.Repositories.EFCore;
using FluentValidation;

namespace Banking.Service.Validations
{
    public class StatementDepositValidator : AbstractValidator<StatementDepositDto>
    {
        public StatementDepositValidator(AccountRepository accountRepository)
        {
            RuleFor(x => x.iban_number)
                .NotEmpty()
                .Matches("([NL]{2})(\\d{2})([A-Z]{4})(\\d{10})")
                .WithMessage("Iban number is invalid")
                .MustAsync((x, y, z) => accountRepository.IsUniqueIbanNumber(x.iban_number))
                .WithMessage("iban number already exists");

            RuleFor(x => x.amount)
                .NotNull()
                .WithMessage("amount can not be null");
        }
    }
}
