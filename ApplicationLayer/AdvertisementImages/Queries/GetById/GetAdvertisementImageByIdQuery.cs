using ApplicationLayer.AdvertisementImages.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.AdvertisementImages.Queries.GetById
{
    public record GetAdvertisementImageByIdQuery(int Id)
        : IRequest<OperationResult<AdvertisementImageResponseDto>>;
}
