using ApplicationLayer.Cat.Interfaces;
using ApplicationLayer.CatReport.DTOs;
using ApplicationLayer.CatReport.Interfaces;
using ApplicationLayer.Common.Interfaces;
using ApplicationLayer.Location.Interfaces;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.CatReport.Commands.CreateCatReport
{
    public class CreateAdvertisementCommandHandler
        : IRequestHandler<CreateAdvertisementCommand, OperationResult<AdvertisementResponseDto>>
    {
        private readonly IAdvertisementRepository _repo;
        private readonly ICatRepository _catRepo;
        private readonly ILocationRepository _locationRepo;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;

        public CreateAdvertisementCommandHandler(
            IAdvertisementRepository repo,
            ICatRepository catRepo,
            ILocationRepository locationRepo,
            IMapper mapper,
            IUserContextService userContext)
        {
            _repo = repo;
            _catRepo = catRepo;
            _locationRepo = locationRepo;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<OperationResult<AdvertisementResponseDto>> Handle(
            CreateAdvertisementCommand request, CancellationToken cancellationToken)
        {
            var accountId = _userContext.AccountId;
            if (accountId is null)
                return OperationResult<AdvertisementResponseDto>.Failure("User not authenticated.");

            var dto = request.Dto;

            var cat = new DomainLayer.Models.Cat
            {
                Name = dto.CatName,
                Breed = dto.Breed,
                FurColor = dto.FurColor,
                AccountId = accountId.Value
            };
            await _catRepo.AddAsync(cat);

            var location = new DomainLayer.Models.Location
            {
                City = dto.City,
                Area = dto.Area
            };
            await _locationRepo.AddAsync(location);

            var ad = new Advertisement
            {
                AccountId = accountId.Value,
                CatId = cat.CatId,
                LocationId = location.LocationId,
                Title = dto.Title,
                Description = dto.Description,
                ContactPhoneNumber = dto.ContactPhoneNumber,
                ContactEmail = dto.ContactEmail,
                LastSeenAt = dto.LastSeenAt,
                Type = dto.Type,
                Status = AdvertisementStatus.Active
            };
            await _repo.AddAsync(ad);

            ad.Cat = cat;
            ad.Location = location;

            return OperationResult<AdvertisementResponseDto>.Success(
                _mapper.Map<AdvertisementResponseDto>(ad));
        }
    }
}
