using ApplicationLayer.CatReport.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.CatReport.Queries.GetMyAdvertisements
{
    public record GetMyAdvertisementsQuery : IRequest<OperationResult<List<AdvertisementResponseDto>>>;
}
