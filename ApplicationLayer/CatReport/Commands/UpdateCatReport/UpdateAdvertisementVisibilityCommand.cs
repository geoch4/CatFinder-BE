using ApplicationLayer.CatReport.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.CatReport.Commands.UpdateCatReport
{
    public record UpdateAdvertisementVisibilityCommand(int Id, bool IsVisible)
        : IRequest<OperationResult<AdvertisementResponseDto>>;
}
