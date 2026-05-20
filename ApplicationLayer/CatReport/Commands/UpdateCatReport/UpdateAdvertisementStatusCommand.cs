using ApplicationLayer.CatReport.DTOs;
using DomainLayer.Models;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.CatReport.Commands.UpdateCatReport
{
    public record UpdateAdvertisementStatusCommand(int Id, AdvertisementStatus Status)
        : IRequest<OperationResult<AdvertisementResponseDto>>;
}
