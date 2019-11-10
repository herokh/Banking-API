using FluentValidation;
using System.Linq;

namespace Banking.Application.Helpers
{
    public static class ValidationHelper
    {
        public static void Validate(IValidator validator, object instance)
        {
            var validationResult = validator.Validate(instance);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.First().ErrorMessage);
            }
        }

    }
}
