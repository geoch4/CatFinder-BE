using DomainLayer.Models;
using FluentValidation;

namespace DomainLayer.Validators
{
    public class AdvertisementValidator : AbstractValidator<Advertisement>
    {
        public AdvertisementValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(x => x.ContactEmail)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.ContactEmail));

            RuleFor(x => x.ContactPhoneNumber)
                .Matches(@"^\+?[0-9\s\-\(\)]{7,20}$")
                .WithMessage("Contact phone number is not valid.")
                .When(x => !string.IsNullOrEmpty(x.ContactPhoneNumber));

            RuleFor(x => x.Type)
                .IsInEnum();

            RuleFor(x => x.Status)
                .IsInEnum();

            RuleFor(x => x.LastSeenAt)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Last seen date cannot be in the future.");
        }
    }
}
