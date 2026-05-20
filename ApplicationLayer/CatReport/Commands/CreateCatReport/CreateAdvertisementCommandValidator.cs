using FluentValidation;

namespace ApplicationLayer.CatReport.Commands.CreateCatReport
{
    public class CreateAdvertisementCommandValidator : AbstractValidator<CreateAdvertisementCommand>
    {
        public CreateAdvertisementCommandValidator()
        {
            RuleFor(x => x.Dto.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Dto.Description).NotEmpty().MaximumLength(2000);
            RuleFor(x => x.Dto.Type).IsInEnum();
            RuleFor(x => x.Dto.ContactEmail).EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Dto.ContactEmail));

            RuleFor(x => x.Dto.Cat).NotNull();
            RuleFor(x => x.Dto.Location).NotNull();

            RuleFor(x => x.Dto.Cat.FurColor)
                .NotEmpty()
                .When(x => x.Dto.Cat is not null);

            RuleFor(x => x.Dto.Location.City)
                .NotEmpty()
                .When(x => x.Dto.Location is not null);
        }


    }
}
