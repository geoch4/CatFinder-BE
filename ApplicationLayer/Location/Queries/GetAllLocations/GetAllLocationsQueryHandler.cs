using ApplicationLayer.Location.DTOs;
using ApplicationLayer.Location.Interfaces;
using AutoMapper;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Location.Queries.GetAllLocations
{
    public class GetAllLocationsQueryHandler
        : IRequestHandler<GetAllLocationsQuery, OperationResult<List<LocationResponseDto>>>
    {
        private readonly ILocationRepository _repo;
        private readonly IMapper _mapper;

        public GetAllLocationsQueryHandler(ILocationRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<LocationResponseDto>>> Handle(
            GetAllLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = await _repo.GetAllAsync();
            return OperationResult<List<LocationResponseDto>>.Success(
                _mapper.Map<List<LocationResponseDto>>(locations));
        }
    }
}
