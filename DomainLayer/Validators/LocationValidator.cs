using DomainLayer.Models;
using FluentValidation;

namespace DomainLayer.Validators
{
    public class LocationValidator : AbstractValidator<Location>
    {
        public LocationValidator()
        {
            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Area)
                .MaximumLength(100)
                .When(x => x.Area != null);

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90m, 90m)
                .When(x => x.Latitude.HasValue);

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180m, 180m)
                .When(x => x.Longitude.HasValue);
        }
    }
}
