using ApplicationLayer.Comments.DTOs;
using ApplicationLayer.Comments.Interfaces;
using ApplicationLayer.Common.Interfaces;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Models.Common;
using MediatR;

namespace ApplicationLayer.Comments.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, OperationResult<CommentResponseDto>>
    {
        private readonly ICommentRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;

        public CreateCommentCommandHandler(ICommentRepository repo, IMapper mapper, IUserContextService userContext)
        {
            _repo = repo;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<OperationResult<CommentResponseDto>> Handle(
            CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var accountId = _userContext.AccountId;
            if (accountId is null)
            {
                return OperationResult<CommentResponseDto>.Failure("User not authenticated.");
            }

            var comment = _mapper.Map<Comment>(request.Dto);
            comment.AccountId = accountId.Value;

            await _repo.AddAsync(comment);

            var createdComment = await _repo.GetByIdWithAccountAsync(comment.CommentId);

            if (createdComment is null)
            {
                return OperationResult<CommentResponseDto>.Failure("Comment could not be created.");
            }

            return OperationResult<CommentResponseDto>.Success(
                _mapper.Map<CommentResponseDto>(createdComment)
            );
        }
    }
}
