using ApplicationLayer.SavedAdvertisements.DTOs;
using ApplicationLayer.SavedAdvertisements.Interfaces;
using ApplicationLayer.SavedAdvertisements.Queries;
using AutoMapper;
using DomainLayer.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLayer.SavedAdvertisements.Handlers
{
    public class GetSavedAdvertisementsByAccoundIdQueryHandler : IRequestHandler<GetSavedAdvertisementByAccoundIdQuery, IEnumerable<SavedAdvertisementResponseDto>>
    {
        private readonly ISavedAdvertisementRepository _savedAdvertisementRepository;
        private readonly IMapper _mapper;

        public GetSavedAdvertisementsByAccoundIdQueryHandler(ISavedAdvertisementRepository savedAdvertisementRepository, IMapper mapper)
        {
            _savedAdvertisementRepository = savedAdvertisementRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SavedAdvertisementResponseDto>> Handle(GetSavedAdvertisementByAccoundIdQuery request, CancellationToken cancellationToken)
        {
            var saved = await _savedAdvertisementRepository.GetByAccountIdAsync(request.accountId);
            return _mapper.Map<IEnumerable<SavedAdvertisementResponseDto>>(saved);
        }

       
    }
}
