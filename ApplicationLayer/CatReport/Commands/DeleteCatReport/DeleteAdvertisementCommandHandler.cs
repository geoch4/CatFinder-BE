using ApplicationLayer.CatReport.Interfaces;
using ApplicationLayer.SavedAdvertisements.Interfaces;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.CatReport.Commands.DeleteCatReport
{
    public class DeleteAdvertisementCommandHandler : IRequestHandler<DeleteAdvertisementCommand, OperationResult<bool>>
    {
        private readonly IAdvertisementRepository _repo;
        private readonly ISavedAdvertisementRepository _savedRepo;

        public DeleteAdvertisementCommandHandler(
            IAdvertisementRepository repo,
            ISavedAdvertisementRepository savedRepo)
        {
            _repo = repo;
            _savedRepo = savedRepo;
        }

        public async Task<OperationResult<bool>> Handle(
            DeleteAdvertisementCommand request, CancellationToken cancellationToken)
        {
            var ad = await _repo.GetByIdAsync(request.Id);
            if (ad is null)
                return OperationResult<bool>.Failure("Advertisement not found.");

            // SavedAdvertisement → Advertisement is Restrict (SQL Server forbids a second
            // cascade path via Account → Advertisement → SavedAdvertisement), so we must
            // remove saved records manually before deleting the advertisement.
            var savedEntries = await _savedRepo.FindAsync(s => s.AdvertisementId == request.Id);
            foreach (var entry in savedEntries)
                await _savedRepo.DeleteAsync(entry);

            await _repo.DeleteAsync(ad);
            return OperationResult<bool>.Success(true);
        }
    }
}
