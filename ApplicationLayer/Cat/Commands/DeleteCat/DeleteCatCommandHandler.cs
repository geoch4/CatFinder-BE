using ApplicationLayer.Cat.Interfaces;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Cat.Commands.DeleteCat
{
    public class DeleteCatCommandHandler : IRequestHandler<DeleteCatCommand, OperationResult<bool>>
    {
        private readonly ICatRepository _repo;

        public DeleteCatCommandHandler(ICatRepository repo) => _repo = repo;

        public async Task<OperationResult<bool>> Handle(DeleteCatCommand request, CancellationToken cancellationToken)
        {
            var cat = await _repo.GetByIdAsync(request.Id);
            if (cat is null)
                return OperationResult<bool>.Failure("Cat not found.");

            await _repo.DeleteAsync(cat);
            return OperationResult<bool>.Success(true);
        }
    }
}
