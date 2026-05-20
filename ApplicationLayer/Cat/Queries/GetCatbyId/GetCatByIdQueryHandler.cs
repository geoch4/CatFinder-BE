using ApplicationLayer.Cat.DTOs;
using ApplicationLayer.Cat.Interfaces;
using AutoMapper;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Cat.Queries.GetCatbyId
{
    public class GetCatByIdQueryHandler : IRequestHandler<GetCatByIdQuery, OperationResult<CatResponseDto>>
    {
        private readonly ICatRepository _repo;
        private readonly IMapper _mapper;

        public GetCatByIdQueryHandler(ICatRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<CatResponseDto>> Handle(GetCatByIdQuery request, CancellationToken cancellationToken)
        {
            var cat = await _repo.GetByIdAsync(request.Id);
            if (cat is null)
                return OperationResult<CatResponseDto>.Failure("Cat not found.");

            return OperationResult<CatResponseDto>.Success(_mapper.Map<CatResponseDto>(cat));
        }
    }
}
