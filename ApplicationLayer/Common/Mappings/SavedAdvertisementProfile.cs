using ApplicationLayer.SavedAdvertisements.DTOs;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayer.Common.Mappings
{
    public class SavedAdvertisementProfile : Profile
    {
        public SavedAdvertisementProfile()
        {
            // SavedAdvertisement → response
            CreateMap<SavedAdvertisement, SavedAdvertisementResponseDto>();

            // Create request → entity
            // AccountId is not in the DTO — it is set from the JWT token in the handler
            CreateMap<CreateSavedAdvertisementDto, SavedAdvertisement>()
                .ForMember(dest => dest.AccountId, opt => opt.Ignore());
        }
    }
}
