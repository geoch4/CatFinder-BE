using DomainLayer.Models;
using FluentValidation;

namespace DomainLayer.Validators
{
    public class AdvertisementImageValidator : AbstractValidator<AdvertisementImage>
    {
        public AdvertisementImageValidator()
        {
            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Image URL must be a valid absolute URL.")
                .MaximumLength(500);
        }
    }
}
