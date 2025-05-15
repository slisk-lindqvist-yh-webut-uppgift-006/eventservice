using Data.Entities;
using Domain.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.DTOs;
using Presentation.Services;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IEventService eventService) : ControllerBase
{
    private readonly IEventService _eventService = eventService;

    [HttpPost]
    public async Task<IActionResult> AddEvent(EventFormDto form)
    {
        var entity = form.MapTo<EventEntity>();

        // Assign extra fields not present in the form (ID, timestamps)
        entity.Id = Guid.NewGuid().ToString();
        entity.CreatedAt = DateTime.UtcNow;
        entity.IsActive = true;
        entity.IsPublic = true;

        var result = await _eventService.AddEvent(entity);

        if (!result.Succeeded)
            return StatusCode(result.StatusCode, result.Error);

        return StatusCode(result.StatusCode, result.Result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _eventService.GetEventsAsync();
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventByIdAsync(string id)
    {
        var result = await _eventService.GetEventByIdAsync(id);
        return Ok(result);
    }
    
    // [HttpGet("{name}")]
    // public async Task<IActionResult> GetEventByNameAsync(string name)
    // {
    //     var result = await _eventService.GetEventByNameAsync(name);
    //     return Ok(result);
    // }
    //
    // [HttpGet("{city}")]
    // public async Task<IActionResult> GetEventsByCityAsync(string city)
    // {
    //     var result = await _eventService.GetEventsByCityAsync(city);
    //     return Ok(result);
    // }
    //
    // [HttpGet("{county}")]
    // public async Task<IActionResult> GetEventsByCountyAsync(string county)
    // {
    //     var result = await _eventService.GetEventsByCountyAsync(county);
    //     return Ok(result);
    // }
    //
    // [HttpGet("{country}")]
    // public async Task<IActionResult> GetEventsByCountryAsync(string country)
    // {
    //     var result = await _eventService.GetEventsByCountryAsync(country);
    //     return Ok(result);
    // }
    
    
    
    
}
