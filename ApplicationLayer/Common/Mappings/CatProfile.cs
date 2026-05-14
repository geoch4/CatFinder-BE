using ApplicationLayer.Cat.DTOs;
using AutoMapper;

namespace ApplicationLayer.Common.Mappings
{
    public class CatProfile : Profile
    {
        public CatProfile()
        {
            // Cat → response
            CreateMap<DomainLayer.Models.Cat, CatResponseDto>();

            // Create request → entity
            CreateMap<CreateCatDto, DomainLayer.Models.Cat>();

            // Update request → entity (only map fields that were actually sent)
            CreateMap<UpdateCatDto, DomainLayer.Models.Cat>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
