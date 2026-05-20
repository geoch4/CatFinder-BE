using ApplicationLayer.CatReport.DTOs;
using DomainLayer.Models;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.CatReport.Queries.GetAllCatReports
{
    public record GetAllAdvertisementsQuery(AdvertisementType? Type, string? City)
        : IRequest<OperationResult<List<AdvertisementResponseDto>>>;
}
