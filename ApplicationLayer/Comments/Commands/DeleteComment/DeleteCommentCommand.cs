using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Comments.Commands.DeleteComment
{
    public record DeleteCommentCommand(int Id) : IRequest<OperationResult<bool>>;
}
