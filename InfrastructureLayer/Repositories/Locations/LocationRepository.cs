using ApplicationLayer.Location.Interfaces;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories.Locations
{
    /// <summary>
    /// EF Core implementation of ILocationRepository.
    /// No extra methods needed beyond the generic ones — locations are only
    /// fetched by id or listed in full for the location picker.
    /// </summary>
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(DbContext context) : base(context) { }
    }
}
