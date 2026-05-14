using DomainLayer.Models;
using FluentValidation;

namespace DomainLayer.Validators
{
    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator()
        {
            RuleFor(x => x.Body)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}
