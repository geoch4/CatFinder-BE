using ApplicationLayer.CatReport.Interfaces;
using ApplicationLayer.Common.Interfaces;
using ApplicationLayer.SavedAdvertisements.Commands;
using ApplicationLayer.SavedAdvertisements.DTOs;
using ApplicationLayer.SavedAdvertisements.Interfaces;
using AutoMapper;
using DomainLayer.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLayer.SavedAdvertisements.Handlers
{
    public class CreateSavedAdvertisementCommandHandler : IRequestHandler<CreateSavedAdvertisementCommand, SavedAdvertisementResponseDto>
    {
        private readonly ISavedAdvertisementRepository _savedAdvertisementRepository;
        private readonly IUserContextService _userContext;
        private readonly IMapper _mapper;

        public CreateSavedAdvertisementCommandHandler(IMapper mapper, ISavedAdvertisementRepository advertisementRepository, IUserContextService userContext)
        {
            _savedAdvertisementRepository = advertisementRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<SavedAdvertisementResponseDto> Handle(
            CreateSavedAdvertisementCommand request,
            CancellationToken cancellationToken)
        {
            var accountId = _userContext.AccountId;

            if(accountId == null)
            {
                throw new UnauthorizedAccessException("User not authenticated.");
            }

            var saved = new SavedAdvertisement
            {
                AccountId = accountId.Value,
                AdvertisementId = request.dto.AdvertisementId,
                CreatedAt = DateTime.UtcNow
            };

            await _savedAdvertisementRepository.AddAsync(saved);

            return _mapper.Map<SavedAdvertisementResponseDto>(saved);
        }

        

    }
}
