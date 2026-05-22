using ApplicationLayer.CatReport.DTOs;
using ApplicationLayer.CatReport.Interfaces;
using ApplicationLayer.Common.Interfaces;
using AutoMapper;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.CatReport.Queries.GetMyAdvertisements
{
    public class GetMyAdvertisementsQueryHandler
        : IRequestHandler<GetMyAdvertisementsQuery, OperationResult<List<AdvertisementResponseDto>>>
    {
        private readonly IAdvertisementRepository _repo;
        private readonly IUserContextService _userContext;
        private readonly IMapper _mapper;

        public GetMyAdvertisementsQueryHandler(
            IAdvertisementRepository repo,
            IUserContextService userContext,
            IMapper mapper)
        {
            _repo = repo;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<AdvertisementResponseDto>>> Handle(
            GetMyAdvertisementsQuery request, CancellationToken cancellationToken)
        {
            var accountId = _userContext.AccountId;
            if (accountId is null)
                return OperationResult<List<AdvertisementResponseDto>>.Failure("Not authenticated.");

            var ads = await _repo.GetByAccountIdAsync(accountId.Value);
            return OperationResult<List<AdvertisementResponseDto>>.Success(
                _mapper.Map<List<AdvertisementResponseDto>>(ads));
        }
    }
}
