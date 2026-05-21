using ApplicationLayer.Comments.DTOs;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayer.Common.Mappings
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            // Comment → response
            CreateMap<Comment, CommentResponseDto>();

            // Create request → entity
            // AccountId is not in the DTO — it is set from the JWT token in the handler
            CreateMap<CreateCommentDto, Comment>()
                .ForMember(dest => dest.AccountId, opt => opt.Ignore());

            // include username when creating a comment
            CreateMap<Comment, CommentResponseDto>()
                .ForMember(dest => dest.Username,
                    opt => opt.MapFrom(src => src.Account.Username));

            // Update request → entity (body is required so no null-skip needed)
            CreateMap<UpdateCommentDto, Comment>();
        }
    }
}
