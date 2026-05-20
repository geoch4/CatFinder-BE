using ApplicationLayer.AdvertisementImages.DTOs;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayer.Common.Mappings
{
    public class AdvertisementImageProfile : Profile
    {
        public AdvertisementImageProfile()
        {
            // AdvertisementImage → response
            CreateMap<AdvertisementImage, AdvertisementImageResponseDto>();

            // Create request → entity
            CreateMap<CreateAdvertisementImageDto, AdvertisementImage>();
        }
    }
}
