using ApplicationLayer.Users.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Users.Queries.GetUserbyId
{
    public record GetAccountByIdQuery(int Id) : IRequest<OperationResult<AccountResponseDto>>;
}
