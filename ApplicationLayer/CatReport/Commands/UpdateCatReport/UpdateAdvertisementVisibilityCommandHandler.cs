using ApplicationLayer.CatReport.DTOs;
using ApplicationLayer.CatReport.Interfaces;
using AutoMapper;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.CatReport.Commands.UpdateCatReport
{
    public class UpdateAdvertisementVisibilityCommandHandler
        : IRequestHandler<UpdateAdvertisementVisibilityCommand, OperationResult<AdvertisementResponseDto>>
    {
        private readonly IAdvertisementRepository _repo;
        private readonly IMapper _mapper;

        public UpdateAdvertisementVisibilityCommandHandler(IAdvertisementRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<AdvertisementResponseDto>> Handle(
            UpdateAdvertisementVisibilityCommand request, CancellationToken cancellationToken)
        {
            var ad = await _repo.GetByIdAsync(request.Id);
            if (ad is null)
                return OperationResult<AdvertisementResponseDto>.Failure("Advertisement not found.");

            ad.IsVisible = request.IsVisible;
            ad.UpdatedAt = DateTime.UtcNow;
            await _repo.UpdateAsync(ad);

            return OperationResult<AdvertisementResponseDto>.Success(
                _mapper.Map<AdvertisementResponseDto>(ad));
        }
    }
}
