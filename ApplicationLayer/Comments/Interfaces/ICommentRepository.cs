using ApplicationLayer.Common.Interfaces;
using DomainLayer.Models;

namespace ApplicationLayer.Comments.Interfaces
{
    /// <summary>
    /// Repository for Comment data access.
    /// Inherits standard CRUD from IGenericRepository and adds advertisement-based lookup.
    /// </summary>
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        /// <summary>
        /// Returns all comments belonging to a specific advertisement, ordered for display.
        /// Maps to GET /api/advertisements/{id}/comments.
        /// </summary>
        Task<IEnumerable<Comment>> GetByAdvertisementIdAsync(int advertisementId);
        Task<Comment?> GetByIdWithAccountAsync(int commentId);
    }
}
