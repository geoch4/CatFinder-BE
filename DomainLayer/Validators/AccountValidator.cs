using DomainLayer.Models;
using FluentValidation;

namespace DomainLayer.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(256);

            RuleFor(x => x.PasswordHash)
                .NotEmpty();

            RuleFor(x => x.Role)
                .IsInEnum();
        }
    }
}
