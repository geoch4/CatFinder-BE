using ApplicationLayer.Cat.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Cat.Commands.CreateCat
{
    public record CreateCatCommand(CreateCatDto Dto) : IRequest<OperationResult<CatResponseDto>>;
}
