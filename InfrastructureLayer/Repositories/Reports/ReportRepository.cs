using ApplicationLayer.Reports.Interfaces;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories.Reports
{
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        public ReportRepository(DbContext context) : base(context) { }

        public async Task<bool> ExistsAsync(int advertisementId, int accountId, int? commentId = null)
            => await _dbSet.AnyAsync(r =>
                r.AdvertisementId == advertisementId &&
                r.AccountId == accountId &&
                r.CommentId == commentId);

        public async Task<IEnumerable<Report>> GetByAdvertisementIdAsync(int advertisementId)
            => await _dbSet.Where(r => r.AdvertisementId == advertisementId).ToListAsync();
    }
}
