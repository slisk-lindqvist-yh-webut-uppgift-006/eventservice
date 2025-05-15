using Data.Entities;
using Data.Repositories;
using Presentation.Models;

namespace Presentation.Services;

public class EventService(IEventRepository eventRepository) : IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;

    #region No Transaction Management

        // public async Task<ServiceResult<EventEntity>> AddEvent(EventEntity entity)
        // {
        //     var existsResult = await _eventRepository.FindAsync(e => e.EventName == entity.EventName);
        //
        //     if (existsResult.Succeeded && existsResult.Result!.Any())
        //     {
        //         return new ServiceResult<EventEntity>
        //         {
        //             Succeeded = false,
        //             StatusCode = 409, // Conflict
        //             Error = "An event with the same name already exists."
        //         };
        //     }
        //
        //     var addResult = await _eventRepository.AddAsync(entity);
        //     if (!addResult.Succeeded)
        //     {
        //         return new ServiceResult<EventEntity>
        //         {
        //             Succeeded = false,
        //             StatusCode = addResult.StatusCode,
        //             Error = addResult.Error
        //         };
        //     }
        //
        //     return new ServiceResult<EventEntity>
        //     {
        //         Succeeded = true,
        //         StatusCode = 201,
        //         Result = entity
        //     };
        // }

    #endregion

    #region Add With Transaction Management (ChatGPT Added by request)

        public async Task<ServiceResult<EventEntity>> AddEvent(EventEntity entity)
        {
            try
            {
                await _eventRepository.BeginTransactionAsync();

                var existsResult = await _eventRepository.FindAsync(e => e.EventName == entity.EventName);

                if (existsResult.Succeeded && existsResult.Result!.Any())
                {
                    await _eventRepository.RollbackTransactionAsync();

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
                    await _eventRepository.RollbackTransactionAsync();

                    return new ServiceResult<EventEntity>
                    {
                        Succeeded = false,
                        StatusCode = addResult.StatusCode,
                        Error = addResult.Error
                    };
                }

                await _eventRepository.CommitTransactionAsync();

                return new ServiceResult<EventEntity>
                {
                    Succeeded = true,
                    StatusCode = 201,
                    Result = entity
                };
            }
            catch (Exception ex)
            {
                await _eventRepository.RollbackTransactionAsync();

                return new ServiceResult<EventEntity>
                {
                    Succeeded = false,
                    StatusCode = 500,
                    Error = $"An error occurred while adding the event: {ex.Message}"
                };
            }
        }


    #endregion

    public async Task<ServiceResult<IEnumerable<EventEntity>>> GetEventsAsync()
    {
        var repoResult = await _eventRepository.GetAllAsync();

        return new ServiceResult<IEnumerable<EventEntity>>
        {
            Succeeded = repoResult.Succeeded,
            StatusCode = repoResult.StatusCode,
            Error = repoResult.Error,
            Result = repoResult.Result ?? []
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
        var repoResult = await _eventRepository.FindAsync(e => e.Tag.TagName == tag);

        return new ServiceResult<IEnumerable<EventEntity>>
        {
            Succeeded = repoResult.Succeeded,
            StatusCode = repoResult.StatusCode,
            Error = repoResult.Error,
            Result = repoResult.Result ?? []
        };
    }
    
    #region Delete With Transaction Management

        public async Task<ServiceResult<object>> DeleteEventAsync(string eventId)
        {
            try
            {
                await _eventRepository.BeginTransactionAsync();

                var getResult = await _eventRepository.GetByIdAsync(eventId);
                if (!getResult.Succeeded || getResult.Result == null)
                {
                    await _eventRepository.RollbackTransactionAsync();

                    return new ServiceResult<object>
                    {
                        Succeeded = false,
                        StatusCode = 404,
                        Error = "Event not found."
                    };
                }

                var deleteResult = await _eventRepository.DeleteAsync(getResult.Result);
                if (!deleteResult.Succeeded)
                {
                    await _eventRepository.RollbackTransactionAsync();

                    return new ServiceResult<object>
                    {
                        Succeeded = false,
                        StatusCode = deleteResult.StatusCode,
                        Error = deleteResult.Error
                    };
                }

                await _eventRepository.CommitTransactionAsync();

                return new ServiceResult<object>
                {
                    Succeeded = true,
                    StatusCode = 204,
                    Result = null
                };
            }
            catch (Exception ex)
            {
                await _eventRepository.RollbackTransactionAsync();

                return new ServiceResult<object>
                {
                    Succeeded = false,
                    StatusCode = 500,
                    Error = $"An error occurred while deleting the event: {ex.Message}"
                };
            }
        }


    #endregion

    #region Update With Transaction Management

        public async Task<ServiceResult<EventEntity>> UpdateEventAsync(EventEntity updatedEntity)
        {
            try
            {
                await _eventRepository.BeginTransactionAsync();

                var getResult = await _eventRepository.GetByIdAsync(updatedEntity.Id);
                if (!getResult.Succeeded || getResult.Result == null)
                {
                    await _eventRepository.RollbackTransactionAsync();

                    return new ServiceResult<EventEntity>
                    {
                        Succeeded = false,
                        StatusCode = 404,
                        Error = "Event not found."
                    };
                }

                // Optional: Check if new name conflicts with another event
                if (updatedEntity.EventName != getResult.Result.EventName)
                {
                    var existsResult = await _eventRepository.FindAsync(e => e.EventName == updatedEntity.EventName);
                    if (existsResult.Succeeded && existsResult.Result!.Any(e => e.Id != updatedEntity.Id))
                    {
                        await _eventRepository.RollbackTransactionAsync();

                        return new ServiceResult<EventEntity>
                        {
                            Succeeded = false,
                            StatusCode = 409,
                            Error = "Another event with the same name already exists."
                        };
                    }
                }

                var updateResult = await _eventRepository.UpdateAsync(updatedEntity);
                if (!updateResult.Succeeded)
                {
                    await _eventRepository.RollbackTransactionAsync();

                    return new ServiceResult<EventEntity>
                    {
                        Succeeded = false,
                        StatusCode = updateResult.StatusCode,
                        Error = updateResult.Error
                    };
                }

                await _eventRepository.CommitTransactionAsync();

                return new ServiceResult<EventEntity>
                {
                    Succeeded = true,
                    StatusCode = 200,
                    Result = updatedEntity
                };
            }
            catch (Exception ex)
            {
                await _eventRepository.RollbackTransactionAsync();

                return new ServiceResult<EventEntity>
                {
                    Succeeded = false,
                    StatusCode = 500,
                    Error = $"An error occurred while updating the event: {ex.Message}"
                };
            }
        }

    #endregion
}