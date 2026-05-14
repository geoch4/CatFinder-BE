using ApplicationLayer.CatReport.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.CatReport.Commands.CreateCatReport
{
    public record CreateAdvertisementCommand(CreateAdvertisementDto Dto)
        : IRequest<OperationResult<AdvertisementResponseDto>>;
}
