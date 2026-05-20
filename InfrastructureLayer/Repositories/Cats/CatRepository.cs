using ApplicationLayer.Cat.Interfaces;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories.Cats
{
    /// <summary>
    /// EF Core implementation of ICatRepository.
    /// Extends GenericRepository with an owner-based lookup.
    /// </summary>
    public class CatRepository : GenericRepository<Cat>, ICatRepository
    {
        public CatRepository(DbContext context) : base(context) { }

        /// <summary>
        /// Returns all cats belonging to the given account.
        /// Used when a user wants to see their registered cats before creating an advertisement.
        /// </summary>
        public async Task<IEnumerable<Cat>> GetByAccountIdAsync(int accountId)
            => await _dbSet.Where(c => c.AccountId == accountId).ToListAsync();
    }
}
