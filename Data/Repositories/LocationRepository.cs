using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface ILocationRepository : IBaseRepository<LocationEntity, string>
{
    // Add location-specific queries if needed
}

public class LocationRepository(DbContext context)
    : BaseRepository<LocationEntity, string>(context), ILocationRepository
{
    
}