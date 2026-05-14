using ApplicationLayer.Users.DTOs;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayer.Common.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            // Account → response (never exposes PasswordHash or RefreshToken)
            CreateMap<Account, AccountResponseDto>();

            // Create request → entity (Password is plain text here; hashing happens in the handler)
            CreateMap<CreateAccountDto, Account>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // Update request → entity (only map fields that were actually sent)
            CreateMap<UpdateAccountDto, Account>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
