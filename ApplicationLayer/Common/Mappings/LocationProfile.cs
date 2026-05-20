using ApplicationLayer.Location.DTOs;
using AutoMapper;

namespace ApplicationLayer.Common.Mappings
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            // Location → response
            CreateMap<DomainLayer.Models.Location, LocationResponseDto>();

            // Create request → entity
            CreateMap<CreateLocationDto, DomainLayer.Models.Location>();

            // Update request → entity (only map fields that were actually sent)
            CreateMap<UpdateLocationDto, DomainLayer.Models.Location>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
