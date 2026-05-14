using ApplicationLayer.Users.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Users.Commands.UpdateUser
{
    public record UpdateAccountCommand(int Id, UpdateAccountDto Dto) : IRequest<OperationResult<AccountResponseDto>>;
}
