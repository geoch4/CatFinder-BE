using ApplicationLayer.Comments.Interfaces;
using ApplicationLayer.Reports.Interfaces;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, OperationResult<bool>>
    {
        private readonly ICommentRepository _repo;
        private readonly IReportRepository _reportRepo;

        public DeleteCommentCommandHandler(ICommentRepository repo, IReportRepository reportRepo)
        {
            _repo = repo;
            _reportRepo = reportRepo;
        }

        public async Task<OperationResult<bool>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _repo.GetByIdAsync(request.Id);
            if (comment is null)
                return OperationResult<bool>.Failure("Comment not found.");

            // CommentId on Report has no FK constraint (avoids multi-cascade-path error),
            // so reports for this comment must be removed manually before deletion.
            var commentReports = await _reportRepo.FindAsync(r => r.CommentId == request.Id);
            foreach (var report in commentReports)
                await _reportRepo.DeleteAsync(report);

            await _repo.DeleteAsync(comment);
            return OperationResult<bool>.Success(true);
        }
    }
}
