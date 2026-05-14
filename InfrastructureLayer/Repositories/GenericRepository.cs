using System.Linq.Expressions;
using ApplicationLayer.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories
{
    /// <summary>
    /// EF Core implementation of IGenericRepository.
    /// All feature repositories inherit from this so they get the standard
    /// CRUD operations without re-implementing them.
    /// </summary>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>Looks up a single row by primary key using EF Core's FindAsync (hits cache before DB).</summary>
        public async Task<T?> GetByIdAsync(int id)
            => await _dbSet.FindAsync(id);

        /// <summary>Fetches every row for this entity type. Avoid on large tables without pagination.</summary>
        public async Task<IEnumerable<T>> GetAllAsync()
            => await _dbSet.ToListAsync();

        /// <summary>Runs a LINQ Where clause against the database and returns matching rows.</summary>
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
            => await _dbSet.Where(predicate).ToListAsync();

        /// <summary>Inserts the entity and immediately calls SaveChangesAsync.</summary>
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>Marks the entity as Modified and immediately calls SaveChangesAsync.</summary>
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>Removes the entity and immediately calls SaveChangesAsync.</summary>
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
