using ApplicationLayer.AdvertisementImages.Interfaces;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories.AdvertisementImages
{
    /// <summary>
    /// EF Core implementation of IAdvertisementImageRepository.
    /// Extends GenericRepository with a lookup that fetches all images for one advertisement.
    /// </summary>
    public class AdvertisementImageRepository : GenericRepository<AdvertisementImage>, IAdvertisementImageRepository
    {
        public AdvertisementImageRepository(DbContext context) : base(context) { }

        /// <summary>
        /// Returns all images attached to a specific advertisement.
        /// Used when loading the photo gallery on an advertisement detail page.
        /// </summary>
        public async Task<IEnumerable<AdvertisementImage>> GetByAdvertisementIdAsync(int advertisementId)
            => await _dbSet.Where(i => i.AdvertisementId == advertisementId).ToListAsync();
    }
}
