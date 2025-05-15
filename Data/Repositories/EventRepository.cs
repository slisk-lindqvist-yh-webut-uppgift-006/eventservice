using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface IEventRepository : IBaseRepository<EventEntity, string>
{
    // Add any custom Event-specific methods here if needed
}

public class EventRepository(DbContext context) 
    : BaseRepository<EventEntity, string>(context), IEventRepository
{
    
}