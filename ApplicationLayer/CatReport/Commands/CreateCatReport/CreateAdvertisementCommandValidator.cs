using FluentValidation;

namespace ApplicationLayer.CatReport.Commands.CreateCatReport
{
    public class CreateAdvertisementCommandValidator : AbstractValidator<CreateAdvertisementCommand>
    {
        public CreateAdvertisementCommandValidator()
        {
            RuleFor(x => x.Dto.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Dto.Description).NotEmpty().MaximumLength(2000);
            RuleFor(x => x.Dto.FurColor).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Dto.City).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Dto.Type).IsInEnum();
            RuleFor(x => x.Dto.ContactEmail).EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Dto.ContactEmail));
        }
    }
}
