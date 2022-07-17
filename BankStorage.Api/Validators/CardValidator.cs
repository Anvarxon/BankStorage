using BankStorage.Api.DTO;
using FluentValidation;

namespace BankStorage.Api.Validators
{
    public class CardValidator : AbstractValidator<CardDto>
    {
        public CardValidator()
        {
            RuleFor(c => c.CardNumber)
                .Cascade(CascadeMode.Stop)
                .Length(16, 16).WithMessage("Длина поля {PropertyName} должен быть 16 знаков")
                .Must(BeNumbersOnly).WithMessage("Поле {PropertyName} имеет не валидные значения. Введите только цифры.");

        }

        protected bool BeNumbersOnly(string card)
        {
            card = card.Replace(" ", "");
            card = card.Replace("-", "");
            return card.All(char.IsNumber);
        }
    }
}
