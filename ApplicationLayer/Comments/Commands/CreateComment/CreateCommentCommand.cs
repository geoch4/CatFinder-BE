using ApplicationLayer.Comments.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Comments.Commands.CreateComment
{
    public record CreateCommentCommand(CreateCommentDto Dto) : IRequest<OperationResult<CommentResponseDto>>;
}
