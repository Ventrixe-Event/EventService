/*
 * Events Controller - REST API Endpoints (EventsController.cs)
 * ============================================================
 *
 * This controller provides RESTful API endpoints for event management
 * in the Ventixe platform. It serves as the presentation layer interface
 * between the frontend application and the business logic.
 *
 * API Endpoints:
 * - GET /api/events - Retrieve all events
 * - GET /api/events/{id} - Retrieve specific event by ID
 * - GET /api/events/category/{category} - Filter events by category
 * - GET /api/events/status/{status} - Filter events by status
 * - GET /api/events/search?searchTerm={term} - Search events by text
 * - POST /api/events - Create new event
 * - PUT /api/events/{id} - Update existing event
 * - DELETE /api/events/{id} - Delete event (soft delete)
 *
 * Features:
 * - Consistent HTTP status code responses
 * - Model validation with detailed error messages
 * - Dependency injection for service layer access
 * - Async operations for better performance
 * - RESTful design principles
 *
 * Development Notes:
 * - AI assistance provided by Claude 4 (Anthropic) for REST API design,
 *   HTTP status code mapping, validation handling, and documentation
 * - Uses primary constructor pattern (C# 12 feature)
 * - Follows controller action naming conventions
 * - Ready for future enhancements like authentication and authorization
 *
 * Author: Kim Hammerstad (with AI assistance from Claude 4)
 * Created: 2024
 */

using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

/// <summary>
/// REST API controller for event management operations.
/// Provides endpoints for creating, reading, updating, and deleting events,
/// as well as advanced filtering and search capabilities.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class EventsController(IEventService eventService) : ControllerBase
{
    /// <summary>
    /// Event service instance injected via primary constructor.
    /// Provides access to business logic layer for event operations.
    /// </summary>
    private readonly IEventService _eventService = eventService;

    /// <summary>
    /// Retrieves all events from the system.
    /// </summary>
    /// <returns>
    /// 200 OK with list of events on success,
    /// 500 Internal Server Error on failure
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _eventService.GetEventsAsync();
        return result.Success ? Ok(result) : StatusCode(500, result);
    }

    /// <summary>
    /// Retrieves a specific event by its unique identifier.
    /// </summary>
    /// <param name="id">The unique event identifier</param>
    /// <returns>
    /// 200 OK with event data on success,
    /// 404 Not Found if event doesn't exist
    /// </returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _eventService.GetEventAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }

    /// <summary>
    /// Retrieves all events belonging to a specific category.
    /// </summary>
    /// <param name="category">The category to filter by (e.g., "Music", "Technology")</param>
    /// <returns>
    /// 200 OK with filtered events on success,
    /// 500 Internal Server Error on failure
    /// </returns>
    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetByCategory(string category)
    {
        var result = await _eventService.GetEventsByCategoryAsync(category);
        return result.Success ? Ok(result) : StatusCode(500, result);
    }

    /// <summary>
    /// Retrieves all events with a specific status.
    /// </summary>
    /// <param name="status">The status to filter by (e.g., "Active", "Draft")</param>
    /// <returns>
    /// 200 OK with filtered events on success,
    /// 500 Internal Server Error on failure
    /// </returns>
    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(string status)
    {
        var result = await _eventService.GetEventsByStatusAsync(status);
        return result.Success ? Ok(result) : StatusCode(500, result);
    }

    /// <summary>
    /// Searches for events based on a text query.
    /// Searches across event titles, descriptions, and other relevant fields.
    /// </summary>
    /// <param name="searchTerm">The text to search for in event data</param>
    /// <returns>
    /// 200 OK with matching events on success,
    /// 400 Bad Request if search term is empty,
    /// 500 Internal Server Error on failure
    /// </returns>
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string searchTerm)
    {
        // Validate search term is provided
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return BadRequest(new { Success = false, Error = "Search term is required" });
        }

        var result = await _eventService.SearchEventsAsync(searchTerm);
        return result.Success ? Ok(result) : StatusCode(500, result);
    }

    /// <summary>
    /// Creates a new event with the provided information.
    /// </summary>
    /// <param name="request">The event creation request containing all event details</param>
    /// <returns>
    /// 201 Created with the new event data and location header on success,
    /// 400 Bad Request if validation fails,
    /// 500 Internal Server Error on failure
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateEventRequest request)
    {
        // Validate the incoming request model
        if (!ModelState.IsValid)
        {
            return BadRequest(
                new
                {
                    Success = false,
                    Error = "Invalid data",
                    Details = ModelState, // Include detailed validation errors
                }
            );
        }

        var result = await _eventService.CreateEventAsync(request);

        // Return 201 Created with location header pointing to the new resource
        return result.Success
            ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result)
            : StatusCode(500, result);
    }

    /// <summary>
    /// Updates an existing event with new information.
    /// </summary>
    /// <param name="id">The unique identifier of the event to update</param>
    /// <param name="request">The updated event information</param>
    /// <returns>
    /// 200 OK with updated event data on success,
    /// 400 Bad Request if validation fails,
    /// 404 Not Found if event doesn't exist
    /// </returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, CreateEventRequest request)
    {
        // Validate the incoming request model
        if (!ModelState.IsValid)
        {
            return BadRequest(
                new
                {
                    Success = false,
                    Error = "Invalid data",
                    Details = ModelState, // Include detailed validation errors
                }
            );
        }

        var result = await _eventService.UpdateEventAsync(id, request);
        return result.Success ? Ok(result) : NotFound(result);
    }

    /// <summary>
    /// Removes an event from the system using soft delete.
    /// The event is marked as inactive rather than physically deleted.
    /// </summary>
    /// <param name="id">The unique identifier of the event to delete</param>
    /// <returns>
    /// 200 OK on successful deletion,
    /// 404 Not Found if event doesn't exist
    /// </returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _eventService.DeleteEventAsync(id);
        return result.Success ? Ok(result) : NotFound(result);
    }
}
