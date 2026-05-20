using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.AdvertisementImages.Commands.DeleteAdvertisementImage
{
    public record DeleteAdvertisementImageCommand(int Id) : IRequest<OperationResult<bool>>;
}
