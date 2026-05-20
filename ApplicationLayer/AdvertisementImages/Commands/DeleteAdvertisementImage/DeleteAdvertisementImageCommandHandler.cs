using ApplicationLayer.AdvertisementImages.Interfaces;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.AdvertisementImages.Commands.DeleteAdvertisementImage
{
    public class DeleteAdvertisementImageCommandHandler
        : IRequestHandler<DeleteAdvertisementImageCommand, OperationResult<bool>>
    {
        private readonly IAdvertisementImageRepository _repo;

        public DeleteAdvertisementImageCommandHandler(IAdvertisementImageRepository repo) => _repo = repo;

        public async Task<OperationResult<bool>> Handle(
            DeleteAdvertisementImageCommand request, CancellationToken cancellationToken)
        {
            var image = await _repo.GetByIdAsync(request.Id);
            if (image is null)
                return OperationResult<bool>.Failure("Advertisement image not found.");

            await _repo.DeleteAsync(image);
            return OperationResult<bool>.Success(true);
        }
    }
}
