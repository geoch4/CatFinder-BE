using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.CatReport.Commands.DeleteCatReport
{
    public record DeleteAdvertisementCommand(int Id) : IRequest<OperationResult<bool>>;
}
