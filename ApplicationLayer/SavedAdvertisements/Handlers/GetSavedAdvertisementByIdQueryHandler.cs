using ApplicationLayer.SavedAdvertisements.DTOs;
using ApplicationLayer.SavedAdvertisements.Interfaces;
using ApplicationLayer.SavedAdvertisements.Queries;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLayer.SavedAdvertisements.Handlers
{
    public class GetSavedAdvertisementByIdQueryHandler : IRequestHandler<GetSavedAdvertisementByIdQuery, SavedAdvertisementResponseDto?>
    {
        private readonly ISavedAdvertisementRepository _savedAdvertisementRepository;
        private readonly IMapper _mapper;

        public GetSavedAdvertisementByIdQueryHandler(IMapper mapper, ISavedAdvertisementRepository savedAdvertisementRepository)
        {
            _savedAdvertisementRepository = savedAdvertisementRepository;
            _mapper = mapper;
        }

        public async Task<SavedAdvertisementResponseDto?> Handle(
            GetSavedAdvertisementByIdQuery request, CancellationToken cancellationToken)
        {
            var saved = await _savedAdvertisementRepository.GetByIdAsync(request.Id);

            return saved is null ? null : _mapper.Map<SavedAdvertisementResponseDto>(saved);
        }
    }
}
