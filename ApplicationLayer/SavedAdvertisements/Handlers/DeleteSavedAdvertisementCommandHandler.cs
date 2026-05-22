using ApplicationLayer.Common.Interfaces;
using ApplicationLayer.SavedAdvertisements.Commands;
using ApplicationLayer.SavedAdvertisements.Interfaces;
using MediatR;

namespace ApplicationLayer.SavedAdvertisements.Handlers
{
    public class DeleteSavedAdvertisementCommandHandler : IRequestHandler<DeleteSavedAdvertisementCommand>
    {
        private readonly ISavedAdvertisementRepository _repo;
        private readonly IUserContextService _userContext;

        public DeleteSavedAdvertisementCommandHandler(
            ISavedAdvertisementRepository repo,
            IUserContextService userContext)
        {
            _repo = repo;
            _userContext = userContext;
        }

        public async Task Handle(DeleteSavedAdvertisementCommand request, CancellationToken cancellationToken)
        {
            var accountId = _userContext.AccountId;
            if (accountId is null) return;

            var entries = await _repo.FindAsync(
                s => s.AdvertisementId == request.Id && s.AccountId == accountId.Value);

            foreach (var entry in entries)
                await _repo.DeleteAsync(entry);
        }
    }
}
