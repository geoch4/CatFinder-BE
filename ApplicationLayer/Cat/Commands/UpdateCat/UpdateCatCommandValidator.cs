using FluentValidation;

namespace ApplicationLayer.Cat.Commands.UpdateCat
{
    public class UpdateCatCommandValidator : AbstractValidator<UpdateCatCommand>
    {
        public UpdateCatCommandValidator()
        {
            RuleFor(x => x.Dto.FurColor).MaximumLength(100);
            RuleFor(x => x.Dto.Name).MaximumLength(100);
            RuleFor(x => x.Dto.Breed).MaximumLength(100);
            RuleFor(x => x.Dto.Description).MaximumLength(1000);
            RuleFor(x => x.Dto.Age)
                .GreaterThan(0).When(x => x.Dto.Age.HasValue)
                .WithMessage("Age must be greater than zero.");
        }
    }
}
