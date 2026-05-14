using ApplicationLayer.Location.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Location.Queries.GetLocationbyId
{
    public record GetLocationByIdQuery(int Id) : IRequest<OperationResult<LocationResponseDto>>;
}
