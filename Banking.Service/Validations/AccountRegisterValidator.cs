using Banking.Application.DTOs;
using Banking.Infrastructure.Repositories.EFCore;
using FluentValidation;

namespace Banking.Service.Validations
{
    public class AccountRegisterValidator : AbstractValidator<AccountRegisterDto>
    {
        public AccountRegisterValidator(AccountRepository accountRepository)
        {
            RuleFor(x => x.iban_number)
                .NotEmpty()
                .Matches("([NL]{2})(\\d{2})([A-Z]{4})(\\d{10})")
                .WithMessage("iban number is invalid")
                .MustAsync((x,y,z) => accountRepository.IsUniqueIbanNumber(x.iban_number))
                .WithMessage("iban number already exists");

            RuleFor(x => x.acccount_name)
                .NotEmpty()
                .WithMessage("account name can not be empty")
                .MinimumLength(8)
                .WithMessage("account name should be at least 8 characters");

        }

    }
}
