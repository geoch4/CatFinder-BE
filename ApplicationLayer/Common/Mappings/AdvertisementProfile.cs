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
            // Cat, Location, and AccountId are created/set by the handler — not mapped here
            CreateMap<CreateAdvertisementDto, Advertisement>()
                .ForMember(dest => dest.AccountId, opt => opt.Ignore())
                .ForMember(dest => dest.CatId, opt => opt.Ignore())
                .ForMember(dest => dest.LocationId, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => AdvertisementStatus.Active));

            // Update request → entity (only map fields that were actually sent)
            CreateMap<UpdateAdvertisementDto, Advertisement>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
