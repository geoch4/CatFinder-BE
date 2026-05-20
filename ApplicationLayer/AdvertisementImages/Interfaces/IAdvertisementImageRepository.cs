using ApplicationLayer.Common.Interfaces;
using DomainLayer.Models;

namespace ApplicationLayer.AdvertisementImages.Interfaces
{
    /// <summary>
    /// Repository for AdvertisementImage data access.
    /// Inherits standard CRUD from IGenericRepository and adds advertisement-based lookup.
    /// </summary>
    public interface IAdvertisementImageRepository : IGenericRepository<AdvertisementImage>
    {
        /// <summary>
        /// Returns all images attached to a specific advertisement.
        /// Used when loading an advertisement's photo gallery.
        /// </summary>
        Task<IEnumerable<AdvertisementImage>> GetByAdvertisementIdAsync(int advertisementId);
    }
}
