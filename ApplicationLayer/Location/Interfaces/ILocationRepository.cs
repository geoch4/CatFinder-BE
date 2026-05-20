using ApplicationLayer.Common.Interfaces;

namespace ApplicationLayer.Location.Interfaces
{
    /// <summary>
    /// Repository for Location data access.
    /// Only needs standard CRUD — no custom queries required for the current API spec.
    /// Locations are looked up by id or listed in full for the location picker UI.
    /// </summary>
    public interface ILocationRepository : IGenericRepository<DomainLayer.Models.Location>
    {
    }
}
