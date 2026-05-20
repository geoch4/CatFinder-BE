using ApplicationLayer.AdvertisementImages.DTOs;
using ApplicationLayer.AdvertisementImages.Interfaces;
using AutoMapper;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.AdvertisementImages.Queries.GetById
{
    public class GetAdvertisementImageByIdQueryHandler
        : IRequestHandler<GetAdvertisementImageByIdQuery, OperationResult<AdvertisementImageResponseDto>>
    {
        private readonly IAdvertisementImageRepository _repo;
        private readonly IMapper _mapper;

        public GetAdvertisementImageByIdQueryHandler(IAdvertisementImageRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<AdvertisementImageResponseDto>> Handle(
            GetAdvertisementImageByIdQuery request, CancellationToken cancellationToken)
        {
            var image = await _repo.GetByIdAsync(request.Id);
            if (image is null)
                return OperationResult<AdvertisementImageResponseDto>.Failure("Advertisement image not found.");

            return OperationResult<AdvertisementImageResponseDto>.Success(
                _mapper.Map<AdvertisementImageResponseDto>(image));
        }
    }
}
