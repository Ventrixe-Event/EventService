using Application.Interfaces;
using Persistence.Interfaces;

namespace Application.Services;

public class EventService(IEventRepository eventRepository) : IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;
}
