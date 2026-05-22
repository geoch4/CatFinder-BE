using ApplicationLayer.Comments.Interfaces;
using ApplicationLayer.Common.Interfaces;
using ApplicationLayer.Reports.DTOs;
using ApplicationLayer.Reports.Interfaces;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Reports.Commands.CreateCommentReport
{
    public class CreateCommentReportCommandHandler
        : IRequestHandler<CreateCommentReportCommand, OperationResult<ReportResponseDto>>
    {
        private readonly IReportRepository _reportRepo;
        private readonly ICommentRepository _commentRepo;
        private readonly IUserContextService _userContext;
        private readonly IMapper _mapper;

        public CreateCommentReportCommandHandler(
            IReportRepository reportRepo,
            ICommentRepository commentRepo,
            IUserContextService userContext,
            IMapper mapper)
        {
            _reportRepo = reportRepo;
            _commentRepo = commentRepo;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<OperationResult<ReportResponseDto>> Handle(
            CreateCommentReportCommand request, CancellationToken cancellationToken)
        {
            var accountId = _userContext.AccountId;
            if (accountId is null)
                return OperationResult<ReportResponseDto>.Failure("Du måste vara inloggad för att rapportera.");

            var comment = await _commentRepo.GetByIdAsync(request.CommentId);
            if (comment is null)
                return OperationResult<ReportResponseDto>.Failure("Comment not found.");

            if (comment.AdvertisementId != request.AdvertisementId)
                return OperationResult<ReportResponseDto>.Failure("Comment does not belong to this advertisement.");

            if (await _reportRepo.ExistsAsync(request.AdvertisementId, accountId.Value, request.CommentId))
                return OperationResult<ReportResponseDto>.Failure("Du har redan rapporterat den här kommentaren.");

            var report = new Report
            {
                AdvertisementId = request.AdvertisementId,
                CommentId = request.CommentId,
                AccountId = accountId.Value,
                Comment = request.Dto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _reportRepo.AddAsync(report);
            return OperationResult<ReportResponseDto>.Success(_mapper.Map<ReportResponseDto>(report));
        }
    }
}
