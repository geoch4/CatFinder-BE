using FluentValidation;

namespace ApplicationLayer.Cat.Commands.CreateCat
{
    public class CreateCatCommandValidator : AbstractValidator<CreateCatCommand>
    {
        public CreateCatCommandValidator()
        {
            RuleFor(x => x.Dto.FurColor)
                .NotEmpty().WithMessage("Fur colour is required.")
                .MaximumLength(100);

            RuleFor(x => x.Dto.Age)
                .GreaterThan(0).When(x => x.Dto.Age.HasValue)
                .WithMessage("Age must be greater than zero.");

            RuleFor(x => x.Dto.ChipNumber)
                .NotEmpty().WithMessage("Chip number is required when the cat is chipped.")
                .When(x => x.Dto.IsChipped);

            RuleFor(x => x.Dto.Name).MaximumLength(100);
            RuleFor(x => x.Dto.Breed).MaximumLength(100);
            RuleFor(x => x.Dto.Description).MaximumLength(1000);
        }
    }
}
