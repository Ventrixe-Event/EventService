using Application.Models;

namespace Application.Interfaces;

public interface IEventService
{
    Task<EventResult<List<EventDto>>> GetEventsAsync();
    Task<EventResult<EventDto>> GetEventAsync(string id);
    Task<EventResult<List<EventDto>>> GetEventsByCategoryAsync(string category);
    Task<EventResult<List<EventDto>>> GetEventsByStatusAsync(string status);
    Task<EventResult<List<EventDto>>> SearchEventsAsync(string searchTerm);
    Task<EventResult<EventDto>> CreateEventAsync(CreateEventRequest request);
    Task<EventResult<EventDto>> UpdateEventAsync(string id, CreateEventRequest request);
    Task<EventResult<bool>> DeleteEventAsync(string id);
}
