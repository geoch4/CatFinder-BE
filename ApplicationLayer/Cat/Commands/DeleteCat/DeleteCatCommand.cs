using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Cat.Commands.DeleteCat
{
    public record DeleteCatCommand(int Id) : IRequest<OperationResult<bool>>;
}
