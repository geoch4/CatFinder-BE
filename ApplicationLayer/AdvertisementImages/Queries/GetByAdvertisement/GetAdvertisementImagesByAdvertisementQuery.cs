using ApplicationLayer.AdvertisementImages.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.AdvertisementImages.Queries.GetByAdvertisement
{
    public record GetAdvertisementImagesByAdvertisementQuery(int AdvertisementId)
        : IRequest<OperationResult<IEnumerable<AdvertisementImageResponseDto>>>;
}
