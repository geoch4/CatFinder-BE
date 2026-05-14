using ApplicationLayer.CatReport.DTOs;
using ApplicationLayer.CatReport.Interfaces;
using AutoMapper;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.CatReport.Queries.GetAllCatReports
{
    public class GetAllAdvertisementsQueryHandler
        : IRequestHandler<GetAllAdvertisementsQuery, OperationResult<List<AdvertisementResponseDto>>>
    {
        private readonly IAdvertisementRepository _repo;
        private readonly IMapper _mapper;

        public GetAllAdvertisementsQueryHandler(IAdvertisementRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<AdvertisementResponseDto>>> Handle(
            GetAllAdvertisementsQuery request, CancellationToken cancellationToken)
        {
            var ads = await _repo.GetFilteredAsync(request.Type, request.City);
            return OperationResult<List<AdvertisementResponseDto>>.Success(
                _mapper.Map<List<AdvertisementResponseDto>>(ads));
        }
    }
}
