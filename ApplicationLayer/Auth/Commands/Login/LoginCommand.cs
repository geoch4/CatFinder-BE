using ApplicationLayer.Auth.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Auth.Commands.Login
{
    public record LoginCommand(LoginDto Dto) : IRequest<OperationResult<AuthResponseDto>>;
}
