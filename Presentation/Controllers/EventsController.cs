using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IEventService eventService) : ControllerBase
{
    private readonly IEventService _eventService = eventService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _eventService.GetEventsAsync();
        return result.Success ? Ok(result) : StatusCode(500, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _eventService.GetEventAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetByCategory(string category)
    {
        var result = await _eventService.GetEventsByCategoryAsync(category);
        return result.Success ? Ok(result) : StatusCode(500, result);
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(string status)
    {
        var result = await _eventService.GetEventsByStatusAsync(status);
        return result.Success ? Ok(result) : StatusCode(500, result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return BadRequest(new { Success = false, Error = "Search term is required" });
        }

        var result = await _eventService.SearchEventsAsync(searchTerm);
        return result.Success ? Ok(result) : StatusCode(500, result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEventRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(
                new
                {
                    Success = false,
                    Error = "Invalid data",
                    Details = ModelState,
                }
            );
        }

        var result = await _eventService.CreateEventAsync(request);
        return result.Success
            ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result)
            : StatusCode(500, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, CreateEventRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(
                new
                {
                    Success = false,
                    Error = "Invalid data",
                    Details = ModelState,
                }
            );
        }

        var result = await _eventService.UpdateEventAsync(id, request);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _eventService.DeleteEventAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }
}
