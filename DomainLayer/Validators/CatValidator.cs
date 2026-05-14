using DomainLayer.Models;
using FluentValidation;

namespace DomainLayer.Validators
{
    public class CatValidator : AbstractValidator<Cat>
    {
        public CatValidator()
        {
            RuleFor(x => x.FurColor)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Name)
                .MaximumLength(100)
                .When(x => x.Name != null);

            RuleFor(x => x.Breed)
                .MaximumLength(100)
                .When(x => x.Breed != null);

            RuleFor(x => x.Age)
                .GreaterThan(0)
                .When(x => x.Age.HasValue);

            RuleFor(x => x.ChipNumber)
                .NotEmpty()
                .WithMessage("Chip number is required when the cat is chipped.")
                .When(x => x.IsChipped);

            RuleFor(x => x.Gender)
                .IsInEnum()
                .When(x => x.Gender.HasValue);

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .When(x => x.Description != null);
        }
    }
}
