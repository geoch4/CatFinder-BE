using ApplicationLayer.CatReport.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.CatReport.Commands.UpdateCatReport
{
    public record UpdateAdvertisementCommand(int Id, UpdateAdvertisementDto Dto)
        : IRequest<OperationResult<AdvertisementResponseDto>>;
}
