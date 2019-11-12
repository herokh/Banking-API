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
                .WithMessage("iban number is invalid")
                .MustAsync((x, y, z) => accountRepository.HasAccount(x.iban_number))
                .WithMessage("account was not found");

            RuleFor(x => x.amount)
                .NotEmpty().WithMessage("amount can not be empty")
                .GreaterThan(0).WithMessage("amount must more than 0");
        }
    }
}
