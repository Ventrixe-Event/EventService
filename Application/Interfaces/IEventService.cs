/*
 * Event Service Interface - Business Logic Contract (IEventService.cs)
 * ===================================================================
 *
 * This interface defines the contract for event management operations
 * in the Ventixe platform. It follows the dependency inversion principle
 * by defining abstractions that the business logic depends on.
 *
 * Service Operations:
 * - Complete CRUD operations for event management
 * - Advanced querying capabilities (by category, status, search)
 * - Consistent return pattern using EventResult<T> wrapper
 * - Asynchronous operations for better performance
 *
 * Design Patterns:
 * - Repository pattern abstraction for data access
 * - Result pattern for consistent error handling
 * - Dependency injection for loose coupling
 *
 * Development Notes:
 * - AI assistance provided by Claude 4 (Anthropic) for interface design,
 *   method signatures, and documentation standards
 * - Designed to support both mock and database implementations
 * - Ready for future enhancements like pagination and caching
 *
 * Author: Kim Hammerstad (with AI assistance from Claude 4)
 * Created: 2024
 */

using Application.Models;

namespace Application.Interfaces;

/// <summary>
/// Defines the contract for event management operations in the Ventixe platform.
/// Provides a comprehensive set of methods for creating, reading, updating, and deleting events,
/// along with advanced querying capabilities for filtering and searching.
/// </summary>
public interface IEventService
{
    /// <summary>
    /// Retrieves all events from the system.
    /// Returns both active and draft events for management purposes.
    /// </summary>
    /// <returns>EventResult containing a list of all events</returns>
    Task<EventResult<List<EventDto>>> GetEventsAsync();

    /// <summary>
    /// Retrieves a specific event by its unique identifier.
    /// Used for detailed event views and editing operations.
    /// </summary>
    /// <param name="id">The unique event identifier</param>
    /// <returns>EventResult containing the requested event or error if not found</returns>
    Task<EventResult<EventDto>> GetEventAsync(string id);

    /// <summary>
    /// Retrieves all events belonging to a specific category.
    /// Enables category-based filtering for improved user experience.
    /// </summary>
    /// <param name="category">The category name to filter by (e.g., "Music", "Technology")</param>
    /// <returns>EventResult containing a list of events in the specified category</returns>
    Task<EventResult<List<EventDto>>> GetEventsByCategoryAsync(string category);

    /// <summary>
    /// Retrieves all events with a specific status.
    /// Allows filtering by event status (e.g., "Active", "Draft", "Cancelled").
    /// </summary>
    /// <param name="status">The status to filter by</param>
    /// <returns>EventResult containing a list of events with the specified status</returns>
    Task<EventResult<List<EventDto>>> GetEventsByStatusAsync(string status);

    /// <summary>
    /// Searches for events based on a text query.
    /// Performs text search across event titles, descriptions, and other relevant fields.
    /// </summary>
    /// <param name="searchTerm">The text to search for in event data</param>
    /// <returns>EventResult containing a list of events matching the search criteria</returns>
    Task<EventResult<List<EventDto>>> SearchEventsAsync(string searchTerm);

    /// <summary>
    /// Creates a new event with the provided information.
    /// Validates the request data and generates a new event with unique identifier.
    /// </summary>
    /// <param name="request">The event creation request containing all event details</param>
    /// <returns>EventResult containing the newly created event or validation errors</returns>
    Task<EventResult<EventDto>> CreateEventAsync(CreateEventRequest request);

    /// <summary>
    /// Updates an existing event with new information.
    /// Preserves the original creation timestamp while updating the modification timestamp.
    /// </summary>
    /// <param name="id">The unique identifier of the event to update</param>
    /// <param name="request">The updated event information</param>
    /// <returns>EventResult containing the updated event or error if not found</returns>
    Task<EventResult<EventDto>> UpdateEventAsync(string id, CreateEventRequest request);

    /// <summary>
    /// Removes an event from the system.
    /// Implements soft delete by setting IsActive to false, preserving data for audit purposes.
    /// </summary>
    /// <param name="id">The unique identifier of the event to delete</param>
    /// <returns>EventResult indicating success or failure of the deletion operation</returns>
    Task<EventResult<bool>> DeleteEventAsync(string id);
}
