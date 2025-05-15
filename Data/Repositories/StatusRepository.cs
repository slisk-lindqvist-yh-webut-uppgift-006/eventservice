using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface IStatusRepository : IBaseRepository<StatusEntity, string>
{
    // Add any custom Event-specific methods here if needed
}

public class StatusRepository(DbContext context) 
    : BaseRepository<StatusEntity, string>(context), IStatusRepository
{
    
}