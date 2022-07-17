using BankStorage.Domain.Models;
using FluentValidation;

namespace BankStorage.Api.Validators
{
    public class BinCodeValidator : AbstractValidator<Bin_Code>
    {
        public BinCodeValidator()
        {
            RuleFor(c => c.BinCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Необходимо заполнить поле {PropertyName}")
                .Length(4, 6).WithMessage("Длина поля {PropertyName} должен быть от 4 до 6 знаков")
                .Must(BeNumbersOnly).WithMessage("Поле {PropertyName} имеет не валидные значения. Введите только цифры.");

            RuleFor(c => c.CardType)
                .Cascade(CascadeMode.Stop)
                .IsInEnum().WithMessage("Enum {PropertyName} не имеет данное значение.");

            RuleFor(c => c.BankId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Необходимо заполнить поле {PropertyName}")
                .NotEqual(0).WithMessage("Поле {PropertyName} не должно равняться нулю");
        }

        protected bool BeNumbersOnly(string binCode)
        {
            binCode = binCode.Replace(" ", "");
            return binCode.All(char.IsNumber);
        }
    }

}
