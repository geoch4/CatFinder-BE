using ApplicationLayer.AdvertisementImages.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.AdvertisementImages.Commands.CreateAdvertisementImage
{
    public record CreateAdvertisementImageCommand(CreateAdvertisementImageDto Dto)
        : IRequest<OperationResult<AdvertisementImageResponseDto>>;
}
