using BankStorage.Domain.Models;
using FluentValidation;

namespace BankStorage.Api.Validators
{
    public class BankValidator : AbstractValidator<Bank>
    {
        public BankValidator()
        {
            RuleFor(c => c.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Поле {PropertyName} не может быть null")
                .NotEmpty().WithMessage("Необходимо заполнить поле {PropertyName}");

        }
    }
}
