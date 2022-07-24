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

            RuleFor(c => c.Logo)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Поле {PropertyName} не может быть null")
                .NotEmpty().WithMessage("Необходимо заполнить поле {PropertyName}")
                .Must(c => c.Equals("image/jpeg") || c.Equals("image/jpg") || c.Equals("image/png")).WithMessage("Форматы только данного типа разрешены для загрузки: JPG, JPEG, PNG");
        }
    }
}
