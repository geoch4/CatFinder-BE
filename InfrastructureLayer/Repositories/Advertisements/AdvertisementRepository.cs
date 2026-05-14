using ApplicationLayer.CatReport.Interfaces;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories.Advertisements
{
    /// <summary>
    /// EF Core implementation of IAdvertisementRepository.
    /// Extends GenericRepository with filtering methods that map to the
    /// GET /api/advertisements query parameters.
    /// </summary>
    public class AdvertisementRepository : GenericRepository<Advertisement>, IAdvertisementRepository
    {
        public AdvertisementRepository(DbContext context) : base(context) { }

        /// <summary>
        /// Returns all advertisements posted by a specific account.
        /// Used on a user's profile page to show their own listings.
        /// </summary>
        public async Task<IEnumerable<Advertisement>> GetByAccountIdAsync(int accountId)
            => await _dbSet.Where(a => a.AccountId == accountId).ToListAsync();

        /// <summary>
        /// Filters by Lost or Found. Maps to GET /api/advertisements?type=Lost.
        /// </summary>
        public async Task<IEnumerable<Advertisement>> GetByTypeAsync(AdvertisementType type)
            => await _dbSet.Where(a => a.Type == type).ToListAsync();

        /// <summary>
        /// Filters by advertisement status (Active, Resolved, Closed).
        /// Typically used internally to show only active listings by default.
        /// </summary>
        public async Task<IEnumerable<Advertisement>> GetByStatusAsync(AdvertisementStatus status)
            => await _dbSet.Where(a => a.Status == status).ToListAsync();

        /// <summary>
        /// Combined filter used by GET /api/advertisements?type=Lost&amp;city=Göteborg.
        /// Includes Location so the city predicate can be evaluated in the database.
        /// </summary>
        public async Task<IEnumerable<Advertisement>> GetFilteredAsync(AdvertisementType? type, string? city)
        {
            var query = _dbSet.Include(a => a.Location).AsQueryable();

            if (type.HasValue)
                query = query.Where(a => a.Type == type.Value);

            if (!string.IsNullOrEmpty(city))
                query = query.Where(a => a.Location.City == city);

            return await query.ToListAsync();
        }
    }
}
