using FluentValidation;

namespace ApplicationLayer.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Dto.UsernameOrEmail).NotEmpty();
            RuleFor(x => x.Dto.Password).NotEmpty();
        }
    }
}
