using ApplicationLayer.Reports.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Reports.Commands.CreateReport
{
    public record CreateReportCommand(int AdvertisementId, CreateReportDto Dto)
        : IRequest<OperationResult<ReportResponseDto>>;
}
