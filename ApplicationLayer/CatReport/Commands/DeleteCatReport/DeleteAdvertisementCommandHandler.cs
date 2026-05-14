using ApplicationLayer.CatReport.Interfaces;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.CatReport.Commands.DeleteCatReport
{
    public class DeleteAdvertisementCommandHandler : IRequestHandler<DeleteAdvertisementCommand, OperationResult<bool>>
    {
        private readonly IAdvertisementRepository _repo;

        public DeleteAdvertisementCommandHandler(IAdvertisementRepository repo) => _repo = repo;

        public async Task<OperationResult<bool>> Handle(
            DeleteAdvertisementCommand request, CancellationToken cancellationToken)
        {
            var ad = await _repo.GetByIdAsync(request.Id);
            if (ad is null)
                return OperationResult<bool>.Failure("Advertisement not found.");

            await _repo.DeleteAsync(ad);
            return OperationResult<bool>.Success(true);
        }
    }
}
