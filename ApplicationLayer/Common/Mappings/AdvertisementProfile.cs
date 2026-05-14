using ApplicationLayer.CatReport.DTOs;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayer.Common.Mappings
{
    public class AdvertisementProfile : Profile
    {
        public AdvertisementProfile()
        {
            // Advertisement → response
            CreateMap<Advertisement, AdvertisementResponseDto>();

            // Create request → entity
            // AccountId is not in the DTO — it is set from the JWT token in the handler
            CreateMap<CreateAdvertisementDto, Advertisement>()
                .ForMember(dest => dest.AccountId, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => AdvertisementStatus.Active));

            // Update request → entity (only map fields that were actually sent)
            CreateMap<UpdateAdvertisementDto, Advertisement>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
