using ApplicationLayer.Common.Interfaces;
using DomainLayer.Models;

namespace ApplicationLayer.Reports.Interfaces
{
    public interface IReportRepository : IGenericRepository<Report>
    {
        Task<bool> ExistsAsync(int advertisementId, int accountId, int? commentId = null);
        Task<IEnumerable<Report>> GetByAdvertisementIdAsync(int advertisementId);
    }
}
