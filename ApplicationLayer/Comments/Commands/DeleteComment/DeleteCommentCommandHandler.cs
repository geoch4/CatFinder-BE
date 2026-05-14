using ApplicationLayer.Comments.Interfaces;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, OperationResult<bool>>
    {
        private readonly ICommentRepository _repo;

        public DeleteCommentCommandHandler(ICommentRepository repo) => _repo = repo;

        public async Task<OperationResult<bool>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _repo.GetByIdAsync(request.Id);
            if (comment is null)
                return OperationResult<bool>.Failure("Comment not found.");

            await _repo.DeleteAsync(comment);
            return OperationResult<bool>.Success(true);
        }
    }
}
