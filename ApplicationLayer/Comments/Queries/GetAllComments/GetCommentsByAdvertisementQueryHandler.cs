using ApplicationLayer.Comments.DTOs;
using ApplicationLayer.Comments.Interfaces;
using AutoMapper;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Comments.Queries.GetAllComments
{
    public class GetCommentsByAdvertisementQueryHandler
        : IRequestHandler<GetCommentsByAdvertisementQuery, OperationResult<List<CommentResponseDto>>>
    {
        private readonly ICommentRepository _repo;
        private readonly IMapper _mapper;

        public GetCommentsByAdvertisementQueryHandler(ICommentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<CommentResponseDto>>> Handle(
            GetCommentsByAdvertisementQuery request, CancellationToken cancellationToken)
        {
            var comments = await _repo.GetByAdvertisementIdAsync(request.AdvertisementId);
            return OperationResult<List<CommentResponseDto>>.Success(
                _mapper.Map<List<CommentResponseDto>>(comments));
        }
    }
}
