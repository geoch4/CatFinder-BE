using ApplicationLayer.Cat.DTOs;
using ApplicationLayer.Cat.Interfaces;
using AutoMapper;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Cat.Commands.UpdateCat
{
    public class UpdateCatCommandHandler : IRequestHandler<UpdateCatCommand, OperationResult<CatResponseDto>>
    {
        private readonly ICatRepository _repo;
        private readonly IMapper _mapper;

        public UpdateCatCommandHandler(ICatRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<CatResponseDto>> Handle(UpdateCatCommand request, CancellationToken cancellationToken)
        {
            var cat = await _repo.GetByIdAsync(request.Id);
            if (cat is null)
                return OperationResult<CatResponseDto>.Failure("Cat not found.");

            _mapper.Map(request.Dto, cat);
            await _repo.UpdateAsync(cat);

            return OperationResult<CatResponseDto>.Success(_mapper.Map<CatResponseDto>(cat));
        }
    }
}
