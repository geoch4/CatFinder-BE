using ApplicationLayer.Comments.Commands.DeleteComment;
using ApplicationLayer.SavedAdvertisements.Commands;
using ApplicationLayer.SavedAdvertisements.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLayer.SavedAdvertisements.Handlers
{
    public class DeleteSavedAdvertisementCommandHandler : IRequestHandler<DeleteSavedAdvertisementCommand>
    {
        private readonly ISavedAdvertisementRepository _savedAdvertisementRepository;

        public DeleteSavedAdvertisementCommandHandler(ISavedAdvertisementRepository savedAdvertisementrepository)
        {
            _savedAdvertisementRepository = savedAdvertisementrepository;
        }

        public async Task Handle(DeleteSavedAdvertisementCommand request, CancellationToken cancellationToken)
        {
            var saved = await _savedAdvertisementRepository.GetByIdAsync(request.Id);

            if(saved == null)
            {
                throw new KeyNotFoundException("Saved advertisement not found.");
            }

            await _savedAdvertisementRepository.DeleteAsync(saved);
        }
    }
}
