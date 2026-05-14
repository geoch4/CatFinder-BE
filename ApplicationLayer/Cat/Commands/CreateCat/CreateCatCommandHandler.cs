using ApplicationLayer.Cat.DTOs;
using ApplicationLayer.Cat.Interfaces;
using ApplicationLayer.Common.Interfaces;
using AutoMapper;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Cat.Commands.CreateCat
{
    public class CreateCatCommandHandler : IRequestHandler<CreateCatCommand, OperationResult<CatResponseDto>>
    {
        private readonly ICatRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;

        public CreateCatCommandHandler(ICatRepository repo, IMapper mapper, IUserContextService userContext)
        {
            _repo = repo;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<OperationResult<CatResponseDto>> Handle(CreateCatCommand request, CancellationToken cancellationToken)
        {
            var accountId = _userContext.AccountId;
            if (accountId is null)
                return OperationResult<CatResponseDto>.Failure("User not authenticated.");

            var cat = _mapper.Map<DomainLayer.Models.Cat>(request.Dto);
            cat.AccountId = accountId.Value;

            await _repo.AddAsync(cat);

            return OperationResult<CatResponseDto>.Success(_mapper.Map<CatResponseDto>(cat));
        }
    }
}
