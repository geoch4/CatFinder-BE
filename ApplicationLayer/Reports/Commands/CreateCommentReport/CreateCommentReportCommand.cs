using ApplicationLayer.Reports.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Reports.Commands.CreateCommentReport
{
    public record CreateCommentReportCommand(int AdvertisementId, int CommentId, CreateReportDto Dto)
        : IRequest<OperationResult<ReportResponseDto>>;
}
