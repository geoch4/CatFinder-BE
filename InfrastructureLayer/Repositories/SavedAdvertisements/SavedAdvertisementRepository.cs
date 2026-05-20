using ApplicationLayer.SavedAdvertisements.Interfaces;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories.SavedAdvertisements
{
    /// <summary>
    /// EF Core implementation of ISavedAdvertisementRepository.
    /// Extends GenericRepository with an account-scoped lookup for the bookmarks feature.
    /// </summary>
    public class SavedAdvertisementRepository : GenericRepository<SavedAdvertisement>, ISavedAdvertisementRepository
    {
        public SavedAdvertisementRepository(DbContext context) : base(context) { }

        /// <summary>
        /// Returns all advertisements saved/bookmarked by a specific account.
        /// Used to display the user's saved listings page.
        /// </summary>
        public async Task<IEnumerable<SavedAdvertisement>> GetByAccountIdAsync(int accountId)
            => await _dbSet.Where(s => s.AccountId == accountId).ToListAsync();
    }
}
