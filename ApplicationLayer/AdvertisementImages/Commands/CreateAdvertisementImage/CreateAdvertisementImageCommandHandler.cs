using ApplicationLayer.AdvertisementImages.DTOs;
using ApplicationLayer.AdvertisementImages.Interfaces;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.AdvertisementImages.Commands.CreateAdvertisementImage
{
    public class CreateAdvertisementImageCommandHandler
        : IRequestHandler<CreateAdvertisementImageCommand, OperationResult<AdvertisementImageResponseDto>>
    {
        private readonly IAdvertisementImageRepository _repo;
        private readonly IMapper _mapper;

        public CreateAdvertisementImageCommandHandler(IAdvertisementImageRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<AdvertisementImageResponseDto>> Handle(
            CreateAdvertisementImageCommand request, CancellationToken cancellationToken)
        {
            var image = _mapper.Map<AdvertisementImage>(request.Dto);
            await _repo.AddAsync(image);

            return OperationResult<AdvertisementImageResponseDto>.Success(
                _mapper.Map<AdvertisementImageResponseDto>(image));
        }
    }
}
