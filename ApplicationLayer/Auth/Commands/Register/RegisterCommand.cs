using ApplicationLayer.Auth.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Auth.Commands.Register
{
    public record RegisterCommand(RegisterDto Dto) : IRequest<OperationResult<AuthResponseDto>>;
}
