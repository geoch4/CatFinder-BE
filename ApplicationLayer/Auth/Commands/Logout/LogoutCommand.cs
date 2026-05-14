using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Auth.Commands.Logout
{
    public record LogoutCommand(int AccountId) : IRequest<OperationResult<bool>>;
}
