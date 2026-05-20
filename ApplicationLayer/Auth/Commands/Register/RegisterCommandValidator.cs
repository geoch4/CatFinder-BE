using FluentValidation;

namespace ApplicationLayer.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Dto.Username).NotEmpty().MinimumLength(3).MaximumLength(50);
            RuleFor(x => x.Dto.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Dto.Password).NotEmpty().MinimumLength(8).MaximumLength(100);
        }
    }
}
