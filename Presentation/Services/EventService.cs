using Data.Entities;
using Data.Repositories;
using Presentation.Models;

namespace Presentation.Services;

public class EventService(IEventRepository eventRepository) : IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;

    public async Task<ServiceResult<EventEntity>> AddEvent(EventEntity entity)
    {
        var existsResult = await _eventRepository.FindAsync(e => e.EventName == entity.EventName);

        if (existsResult.Succeeded && existsResult.Result!.Any())
        {
            return new ServiceResult<EventEntity>
            {
                Succeeded = false,
                StatusCode = 409, // Conflict
                Error = "An event with the same name already exists."
            };
        }

        var addResult = await _eventRepository.AddAsync(entity);
        if (!addResult.Succeeded)
        {
            return new ServiceResult<EventEntity>
            {
                Succeeded = false,
                StatusCode = addResult.StatusCode,
                Error = addResult.Error
            };
        }

        return new ServiceResult<EventEntity>
        {
            Succeeded = true,
            StatusCode = 201,
            Result = entity
        };
    }

    public async Task<ServiceResult<IEnumerable<EventEntity>>> GetEventsAsync()
    {
        var repoResult = await _eventRepository.GetAllAsync();

        return new ServiceResult<IEnumerable<EventEntity>>
        {
            Succeeded = repoResult.Succeeded,
            StatusCode = repoResult.StatusCode,
            Error = repoResult.Error,
            Result = repoResult.Result ?? Enumerable.Empty<EventEntity>()
        };
    }

    public async Task<ServiceResult<EventEntity?>> GetEventByIdAsync(string eventId)
    {
        var repoResult = await _eventRepository.GetByIdAsync(eventId);

        return new ServiceResult<EventEntity?>
        {
            Succeeded = repoResult.Succeeded,
            StatusCode = repoResult.StatusCode,
            Error = repoResult.Error,
            Result = repoResult.Result
        };
    }
    
    public async Task<ServiceResult<EventEntity?>> GetEventByNameAsync(string eventName)
    {
        var repoResult = await _eventRepository.FindAsync(e => e.EventName == eventName);

        return new ServiceResult<EventEntity?>
        {
            Succeeded = repoResult.Succeeded,
            StatusCode = repoResult.StatusCode,
            Error = repoResult.Error,
            Result = repoResult.Result?.FirstOrDefault()
        };
    }
    
    public async Task<ServiceResult<IEnumerable<EventEntity>>> GetEventsByDateAsync(DateTime date)
    {
        var repoResult = await _eventRepository.FindAsync(e => e.StartDate.Date == date.Date);

        return new ServiceResult<IEnumerable<EventEntity>>
        {
            Succeeded = repoResult.Succeeded,
            StatusCode = repoResult.StatusCode,
            Error = repoResult.Error,
            Result = repoResult.Result ?? []
        };
    }

    public async Task<ServiceResult<IEnumerable<EventEntity>>> GetEventsByCityAsync(string location)
    {
        var repoResult = await _eventRepository.FindAsync(e => e.Location.City == location);

        return new ServiceResult<IEnumerable<EventEntity>>
        {
            Succeeded = repoResult.Succeeded,
            StatusCode = repoResult.StatusCode,
            Error = repoResult.Error,
            Result = repoResult.Result ?? []
        };
    }
    
    public async Task<ServiceResult<IEnumerable<EventEntity>>> GetEventsByCountyAsync(string location)
    {
        var repoResult = await _eventRepository.FindAsync(e => e.Location.County == location);

        return new ServiceResult<IEnumerable<EventEntity>>
        {
            Succeeded = repoResult.Succeeded,
            StatusCode = repoResult.StatusCode,
            Error = repoResult.Error,
            Result = repoResult.Result ?? []
        };
    }
    
    public async Task<ServiceResult<IEnumerable<EventEntity>>> GetEventsByCountryAsync(string location)
    {
        var repoResult = await _eventRepository.FindAsync(e => e.Location.Country == location);

        return new ServiceResult<IEnumerable<EventEntity>>
        {
            Succeeded = repoResult.Succeeded,
            StatusCode = repoResult.StatusCode,
            Error = repoResult.Error,
            Result = repoResult.Result ?? []
        };
    }

    public async Task<ServiceResult<IEnumerable<EventEntity>>> GetEventsByTagAsync(string tag)
    {
        var repoResult = await _eventRepository.FindAsync(e => e.Tags.Any(t => t.TagName == tag));

        return new ServiceResult<IEnumerable<EventEntity>>
        {
            Succeeded = repoResult.Succeeded,
            StatusCode = repoResult.StatusCode,
            Error = repoResult.Error,
            Result = repoResult.Result ?? []
        };
    }
}