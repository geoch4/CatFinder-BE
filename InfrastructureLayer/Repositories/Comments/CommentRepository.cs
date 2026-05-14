using ApplicationLayer.Comments.Interfaces;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories.Comments
{
    /// <summary>
    /// EF Core implementation of ICommentRepository.
    /// Extends GenericRepository with an advertisement-scoped lookup.
    /// </summary>
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DbContext context) : base(context) { }

        /// <summary>
        /// Returns all comments for a specific advertisement.
        /// Maps to GET /api/advertisements/{id}/comments.
        /// </summary>
        public async Task<IEnumerable<Comment>> GetByAdvertisementIdAsync(int advertisementId)
            => await _dbSet.Where(c => c.AdvertisementId == advertisementId).ToListAsync();
    }
}
