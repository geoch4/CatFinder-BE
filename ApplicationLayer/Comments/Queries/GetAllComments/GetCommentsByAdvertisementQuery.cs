using ApplicationLayer.Comments.DTOs;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Comments.Queries.GetAllComments
{
    public record GetCommentsByAdvertisementQuery(int AdvertisementId)
        : IRequest<OperationResult<List<CommentResponseDto>>>;
}
