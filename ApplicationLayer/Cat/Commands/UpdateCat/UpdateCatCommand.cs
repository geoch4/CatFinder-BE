using ApplicationLayer.Cat.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Cat.Commands.UpdateCat
{
    public record UpdateCatCommand(int Id, UpdateCatDto Dto) : IRequest<OperationResult<CatResponseDto>>;
}
