using Data.Entities;
using Presentation.Models;

namespace Presentation.Services;

public interface IEventService
{
    Task<ServiceResult<EventEntity>> AddEvent(EventEntity entity);
    Task<ServiceResult<IEnumerable<EventEntity>>> GetEventsAsync();
    Task<ServiceResult<EventEntity?>> GetEventByIdAsync(string eventId);
}