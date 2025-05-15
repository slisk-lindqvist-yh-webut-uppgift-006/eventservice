using Data.Entities;
using Presentation.Models;

namespace Presentation.Services;

public interface IEventService
{
    Task<ServiceResult<EventEntity>> AddEvent(EventEntity entity);
    Task<ServiceResult<IEnumerable<EventEntity>>> GetEventsAsync();
    Task<ServiceResult<EventEntity?>> GetEventByIdAsync(string eventId);
    Task<ServiceResult<EventEntity?>> GetEventByNameAsync(string eventName);
    Task<ServiceResult<IEnumerable<EventEntity>>> GetEventsByDateAsync(DateTime date);
    Task<ServiceResult<IEnumerable<EventEntity>>> GetEventsByCityAsync(string location);
    Task<ServiceResult<IEnumerable<EventEntity>>> GetEventsByCountyAsync(string location);
    Task<ServiceResult<IEnumerable<EventEntity>>> GetEventsByCountryAsync(string location);
    Task<ServiceResult<IEnumerable<EventEntity>>> GetEventsByTagAsync(string tag);
    Task<ServiceResult<object>> DeleteEventAsync(string eventId);
    Task<ServiceResult<EventEntity>> UpdateEventAsync(EventEntity updatedEntity);
}