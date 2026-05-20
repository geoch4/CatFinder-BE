using System.Security.Claims;
using ApplicationLayer.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace InfrastructureLayer.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? AccountId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?.User
                    .FindFirstValue(ClaimTypes.NameIdentifier);
                return int.TryParse(value, out var id) ? id : null;
            }
        }
    }
}
