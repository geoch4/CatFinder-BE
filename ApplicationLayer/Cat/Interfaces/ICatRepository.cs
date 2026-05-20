using ApplicationLayer.Common.Interfaces;

namespace ApplicationLayer.Cat.Interfaces
{
    /// <summary>
    /// Repository for Cat data access.
    /// Inherits standard CRUD from IGenericRepository and adds owner-based lookup.
    /// </summary>
    public interface ICatRepository : IGenericRepository<DomainLayer.Models.Cat>
    {
        /// <summary>
        /// Returns all cats registered under a specific account.
        /// Used when an owner wants to see their own cats.
        /// </summary>
        Task<IEnumerable<DomainLayer.Models.Cat>> GetByAccountIdAsync(int accountId);
    }
}
