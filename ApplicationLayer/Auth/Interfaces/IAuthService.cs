using DomainLayer.Models;

namespace ApplicationLayer.Auth.Interfaces
{
    public interface IAuthService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
        (string token, DateTime expiresAt) GenerateJwtToken(Account account);
        string GenerateRefreshToken();
    }
}
