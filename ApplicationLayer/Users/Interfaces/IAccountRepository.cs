using ApplicationLayer.Common.Interfaces;
using DomainLayer.Models;

namespace ApplicationLayer.Users.Interfaces
{
    /// <summary>
    /// Repository for Account (user) data access.
    /// Inherits standard CRUD from IGenericRepository and adds login/auth-specific lookups.
    /// </summary>
    public interface IAccountRepository : IGenericRepository<Account>
    {
        /// <summary>
        /// Finds an account by email address.
        /// Used during login, registration duplicate-check, and password reset.
        /// </summary>
        Task<Account?> GetByEmailAsync(string email);

        /// <summary>
        /// Finds an account by username.
        /// Used during login and registration duplicate-check.
        /// </summary>
        Task<Account?> GetByUsernameAsync(string username);

        /// <summary>
        /// Finds an account by its stored refresh token.
        /// Used during the token-refresh flow to validate and rotate the refresh token.
        /// </summary>
        Task<Account?> GetByRefreshTokenAsync(string refreshToken);
    }
}
