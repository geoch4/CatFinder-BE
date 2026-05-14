using ApplicationLayer.Auth.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Auth.Commands.RefreshToken
{
    public record RefreshTokenCommand(string Token) : IRequest<OperationResult<AuthResponseDto>>;
}
