using ApplicationLayer.Location.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Location.Queries.GetAllLocations
{
    public record GetAllLocationsQuery : IRequest<OperationResult<List<LocationResponseDto>>>;
}
