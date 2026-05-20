using ApplicationLayer.AdvertisementImages.DTOs;
using ApplicationLayer.AdvertisementImages.Interfaces;
using AutoMapper;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.AdvertisementImages.Queries.GetByAdvertisement
{
    public class GetAdvertisementImagesByAdvertisementQueryHandler
        : IRequestHandler<GetAdvertisementImagesByAdvertisementQuery, OperationResult<IEnumerable<AdvertisementImageResponseDto>>>
    {
        private readonly IAdvertisementImageRepository _repo;
        private readonly IMapper _mapper;

        public GetAdvertisementImagesByAdvertisementQueryHandler(IAdvertisementImageRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<IEnumerable<AdvertisementImageResponseDto>>> Handle(
            GetAdvertisementImagesByAdvertisementQuery request, CancellationToken cancellationToken)
        {
            var images = await _repo.GetByAdvertisementIdAsync(request.AdvertisementId);
            return OperationResult<IEnumerable<AdvertisementImageResponseDto>>.Success(
                _mapper.Map<IEnumerable<AdvertisementImageResponseDto>>(images));
        }
    }
}
