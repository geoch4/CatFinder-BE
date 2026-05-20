using ApplicationLayer.Location.DTOs;
using ApplicationLayer.Location.Interfaces;
using AutoMapper;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Location.Queries.GetLocationbyId
{
    public class GetLocationByIdQueryHandler
        : IRequestHandler<GetLocationByIdQuery, OperationResult<LocationResponseDto>>
    {
        private readonly ILocationRepository _repo;
        private readonly IMapper _mapper;

        public GetLocationByIdQueryHandler(ILocationRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<LocationResponseDto>> Handle(
            GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var location = await _repo.GetByIdAsync(request.Id);
            if (location is null)
                return OperationResult<LocationResponseDto>.Failure("Location not found.");

            return OperationResult<LocationResponseDto>.Success(
                _mapper.Map<LocationResponseDto>(location));
        }
    }
}
