using FluentValidation;

namespace ApplicationLayer.Comments.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.Dto.AdvertisementId).GreaterThan(0);
            RuleFor(x => x.Dto.Body).NotEmpty().MaximumLength(1000);
        }
    }
}
