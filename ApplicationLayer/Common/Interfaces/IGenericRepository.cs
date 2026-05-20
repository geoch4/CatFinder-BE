using System.Linq.Expressions;

namespace ApplicationLayer.Common.Interfaces
{
    /// <summary>
    /// Base repository contract shared by every entity.
    /// All feature repositories extend this so every entity gets these operations for free.
    /// </summary>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>Returns a single entity by its primary key, or null if not found.</summary>
        Task<T?> GetByIdAsync(int id);

        /// <summary>Returns every row for this entity — use with care on large tables.</summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>Returns all rows that match the given LINQ predicate.</summary>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>Inserts a new entity and saves to the database.</summary>
        Task AddAsync(T entity);

        /// <summary>Marks an existing entity as modified and saves changes.</summary>
        Task UpdateAsync(T entity);

        /// <summary>Removes an entity from the database.</summary>
        Task DeleteAsync(T entity);
    }
}
