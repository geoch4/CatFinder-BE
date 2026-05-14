using ApplicationLayer.Auth.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Auth.Commands.ResetPassword
{
    public record ResetPasswordCommand(ResetPasswordDto Dto) : IRequest<OperationResult<bool>>;
}
