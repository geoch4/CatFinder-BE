using ApplicationLayer.Users.Interfaces;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Auth.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, OperationResult<bool>>
    {
        private readonly IAccountRepository _repo;

        public LogoutCommandHandler(IAccountRepository repo) => _repo = repo;

        public async Task<OperationResult<bool>> Handle(
            LogoutCommand request, CancellationToken cancellationToken)
        {
            var account = await _repo.GetByIdAsync(request.AccountId);
            if (account is null)
                return OperationResult<bool>.Failure("Account not found.");

            account.RefreshToken = null;
            account.RefreshTokenExpiresAt = null;
            await _repo.UpdateAsync(account);

            return OperationResult<bool>.Success(true);
        }
    }
}
