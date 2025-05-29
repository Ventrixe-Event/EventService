using Application.Interfaces;
using Application.Models;

namespace Application.Services;

public class EventService : IEventService
{
    // Mock data based on the events shown in the user's frontend
    private readonly List<EventDto> _mockEvents = new()
    {
        new EventDto
        {
            Id = "1",
            Title = "Adventure Gear Show",
            Description = "Discover the latest in outdoor adventure equipment and gear",
            Category = "Outdoor & Adventure",
            Location = "Rocky Mountain Exhibition Hall, Denver, CO",
            StartDate = new DateTime(2026, 6, 5),
            StartTime = new TimeSpan(17, 0, 0), // 5:00 PM
            Price = 40m,
            Status = "Active",
            Progress = 65,
            MaxAttendees = 500,
            CurrentAttendees = 325,
            OrganizerName = "Mountain Adventures Inc",
            CreatedAt = DateTime.UtcNow.AddDays(-30),
            UpdatedAt = DateTime.UtcNow.AddDays(-5),
            IsActive = true,
        },
        new EventDto
        {
            Id = "2",
            Title = "Symphony Under the Stars",
            Description = "A magical evening of classical music under the night sky",
            Category = "Music",
            Location = "Sunset Park, Los Angeles, CA",
            StartDate = new DateTime(2026, 4, 29),
            StartTime = new TimeSpan(19, 0, 0), // 7:00 PM
            Price = 50m,
            Status = "Active",
            Progress = 75,
            MaxAttendees = 800,
            CurrentAttendees = 600,
            OrganizerName = "LA Symphony Orchestra",
            CreatedAt = DateTime.UtcNow.AddDays(-45),
            UpdatedAt = DateTime.UtcNow.AddDays(-3),
            IsActive = true,
        },
        new EventDto
        {
            Id = "3",
            Title = "Runway Revolution 2029",
            Description = "The latest fashion trends and designer collections",
            Category = "Fashion",
            Location = "Vogue Hall, New York, NY",
            StartDate = new DateTime(2026, 5, 1),
            StartTime = new TimeSpan(18, 0, 0), // 6:00 PM
            Price = 100m,
            Status = "Active",
            Progress = 50,
            MaxAttendees = 300,
            CurrentAttendees = 150,
            OrganizerName = "Fashion Forward Events",
            CreatedAt = DateTime.UtcNow.AddDays(-60),
            UpdatedAt = DateTime.UtcNow.AddDays(-10),
            IsActive = true,
        },
        new EventDto
        {
            Id = "4",
            Title = "Global Wellness Summit",
            Description = "Leading experts discuss health and wellness trends",
            Category = "Health & Wellness",
            Location = "Wellness Arena, Miami, FL",
            StartDate = new DateTime(2026, 5, 5),
            StartTime = new TimeSpan(9, 0, 0), // 9:00 AM
            Price = 75m,
            Status = "Active",
            Progress = 40,
            MaxAttendees = 1000,
            CurrentAttendees = 400,
            OrganizerName = "Global Wellness Institute",
            CreatedAt = DateTime.UtcNow.AddDays(-25),
            UpdatedAt = DateTime.UtcNow.AddDays(-2),
            IsActive = true,
        },
        new EventDto
        {
            Id = "5",
            Title = "Artistry Unveiled Expo",
            Description = "Contemporary art exhibition featuring emerging artists",
            Category = "Art & Design",
            Location = "Modern Art Gallery, Chicago, IL",
            StartDate = new DateTime(2026, 5, 15),
            StartTime = new TimeSpan(10, 0, 0), // 10:00 AM
            Price = 20m,
            Status = "Active",
            Progress = 85,
            MaxAttendees = 400,
            CurrentAttendees = 340,
            OrganizerName = "Chicago Art Collective",
            CreatedAt = DateTime.UtcNow.AddDays(-20),
            UpdatedAt = DateTime.UtcNow.AddDays(-1),
            IsActive = true,
        },
        new EventDto
        {
            Id = "6",
            Title = "Culinary Delights Festival",
            Description = "A celebration of international cuisine and culinary arts",
            Category = "Food & Culinary",
            Location = "Gourmet Plaza, San Francisco, CA",
            StartDate = new DateTime(2026, 5, 25),
            StartTime = new TimeSpan(11, 0, 0), // 11:00 AM
            Price = 45m,
            Status = "Active",
            Progress = 60,
            MaxAttendees = 600,
            CurrentAttendees = 360,
            OrganizerName = "SF Culinary Society",
            CreatedAt = DateTime.UtcNow.AddDays(-35),
            UpdatedAt = DateTime.UtcNow.AddDays(-7),
            IsActive = true,
        },
        new EventDto
        {
            Id = "7",
            Title = "Echo Beats Festival",
            Description = "Electronic music festival featuring top DJs and artists",
            Category = "Music",
            Location = "Sunset Park, Los Angeles, CA",
            StartDate = new DateTime(2026, 5, 29),
            StartTime = new TimeSpan(18, 0, 0), // 6:00 PM
            Price = 60m,
            Status = "Active",
            Progress = 70,
            MaxAttendees = 1200,
            CurrentAttendees = 840,
            OrganizerName = "Echo Entertainment",
            CreatedAt = DateTime.UtcNow.AddDays(-40),
            UpdatedAt = DateTime.UtcNow.AddDays(-4),
            IsActive = true,
        },
        new EventDto
        {
            Id = "8",
            Title = "Tech Future Expo",
            Description = "Exploring the latest innovations in technology",
            Category = "Technology",
            Location = "Silicon Valley, San Jose, CA",
            StartDate = new DateTime(2026, 6, 1),
            StartTime = new TimeSpan(10, 0, 0), // 10:00 AM
            Price = 80m,
            Status = "Active",
            Progress = 55,
            MaxAttendees = 800,
            CurrentAttendees = 440,
            OrganizerName = "Tech Innovators",
            CreatedAt = DateTime.UtcNow.AddDays(-15),
            UpdatedAt = DateTime.UtcNow.AddDays(-1),
            IsActive = true,
        },
        // Draft Events
        new EventDto
        {
            Id = "9",
            Title = "Digital Marketing Conference",
            Description = "Learn the latest digital marketing strategies and trends",
            Category = "Business",
            Location = "Marketing Hub, Austin, TX",
            StartDate = new DateTime(2026, 7, 10),
            StartTime = new TimeSpan(9, 0, 0), // 9:00 AM
            Price = 120m,
            Status = "Draft",
            Progress = 15,
            MaxAttendees = 200,
            CurrentAttendees = 0,
            OrganizerName = "Digital Growth Agency",
            CreatedAt = DateTime.UtcNow.AddDays(-5),
            UpdatedAt = DateTime.UtcNow.AddDays(-1),
            IsActive = true,
        },
        new EventDto
        {
            Id = "10",
            Title = "Photography Workshop",
            Description = "Master the art of landscape photography",
            Category = "Art & Design",
            Location = "Mountain View Studio, Colorado",
            StartDate = new DateTime(2026, 8, 15),
            StartTime = new TimeSpan(8, 0, 0), // 8:00 AM
            Price = 85m,
            Status = "Draft",
            Progress = 25,
            MaxAttendees = 50,
            CurrentAttendees = 0,
            OrganizerName = "Photo Masters Academy",
            CreatedAt = DateTime.UtcNow.AddDays(-3),
            UpdatedAt = DateTime.UtcNow,
            IsActive = true,
        },
        // Past Events
        new EventDto
        {
            Id = "11",
            Title = "Winter Film Festival",
            Description = "Independent films from around the world",
            Category = "Entertainment",
            Location = "Cinema District, Portland, OR",
            StartDate = new DateTime(2024, 12, 15),
            StartTime = new TimeSpan(18, 0, 0), // 6:00 PM
            Price = 35m,
            Status = "Past",
            Progress = 100,
            MaxAttendees = 300,
            CurrentAttendees = 285,
            OrganizerName = "Portland Film Society",
            CreatedAt = DateTime.UtcNow.AddDays(-180),
            UpdatedAt = DateTime.UtcNow.AddDays(-90),
            IsActive = false,
        },
        new EventDto
        {
            Id = "12",
            Title = "Holiday Market 2024",
            Description = "Local artisans and holiday gifts",
            Category = "Shopping",
            Location = "Town Square, Burlington, VT",
            StartDate = new DateTime(2024, 12, 20),
            StartTime = new TimeSpan(10, 0, 0), // 10:00 AM
            Price = 0m,
            Status = "Past",
            Progress = 100,
            MaxAttendees = 1000,
            CurrentAttendees = 950,
            OrganizerName = "Burlington Chamber of Commerce",
            CreatedAt = DateTime.UtcNow.AddDays(-200),
            UpdatedAt = DateTime.UtcNow.AddDays(-100),
            IsActive = false,
        },
    };

    public async Task<EventResult<List<EventDto>>> GetEventsAsync()
    {
        try
        {
            await Task.Delay(100); // Simulate async operation
            return EventResult<List<EventDto>>.SuccessResult(_mockEvents);
        }
        catch (Exception ex)
        {
            return EventResult<List<EventDto>>.FailureResult(ex.Message);
        }
    }

    public async Task<EventResult<EventDto>> GetEventAsync(string id)
    {
        try
        {
            await Task.Delay(50); // Simulate async operation
            var eventItem = _mockEvents.FirstOrDefault(e => e.Id == id);

            if (eventItem == null)
            {
                return EventResult<EventDto>.FailureResult("Event not found");
            }

            return EventResult<EventDto>.SuccessResult(eventItem);
        }
        catch (Exception ex)
        {
            return EventResult<EventDto>.FailureResult(ex.Message);
        }
    }

    public async Task<EventResult<List<EventDto>>> GetEventsByCategoryAsync(string category)
    {
        try
        {
            await Task.Delay(100); // Simulate async operation
            var events = _mockEvents
                .Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return EventResult<List<EventDto>>.SuccessResult(events);
        }
        catch (Exception ex)
        {
            return EventResult<List<EventDto>>.FailureResult(ex.Message);
        }
    }

    public async Task<EventResult<List<EventDto>>> GetEventsByStatusAsync(string status)
    {
        try
        {
            await Task.Delay(100); // Simulate async operation
            var events = _mockEvents
                .Where(e => e.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return EventResult<List<EventDto>>.SuccessResult(events);
        }
        catch (Exception ex)
        {
            return EventResult<List<EventDto>>.FailureResult(ex.Message);
        }
    }

    public async Task<EventResult<List<EventDto>>> SearchEventsAsync(string searchTerm)
    {
        try
        {
            await Task.Delay(100); // Simulate async operation
            var events = _mockEvents
                .Where(e =>
                    e.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                    || e.Description!.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                    || e.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                    || e.Location.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                )
                .ToList();

            return EventResult<List<EventDto>>.SuccessResult(events);
        }
        catch (Exception ex)
        {
            return EventResult<List<EventDto>>.FailureResult(ex.Message);
        }
    }

    public async Task<EventResult<EventDto>> CreateEventAsync(CreateEventRequest request)
    {
        try
        {
            await Task.Delay(200); // Simulate async operation

            var newEvent = new EventDto
            {
                Id = Guid.NewGuid().ToString(),
                Title = request.Title,
                Description = request.Description,
                Category = request.Category,
                Location = request.Location,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Price = request.Price,
                Status = "Active",
                Progress = 0,
                MaxAttendees = request.MaxAttendees,
                CurrentAttendees = 0,
                ImageUrl = request.ImageUrl,
                OrganizerName = request.OrganizerName,
                OrganizerEmail = request.OrganizerEmail,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true,
            };

            _mockEvents.Add(newEvent);
            return EventResult<EventDto>.SuccessResult(newEvent, "Event created successfully");
        }
        catch (Exception ex)
        {
            return EventResult<EventDto>.FailureResult(ex.Message);
        }
    }

    public async Task<EventResult<EventDto>> UpdateEventAsync(string id, CreateEventRequest request)
    {
        try
        {
            await Task.Delay(200); // Simulate async operation

            var existingEvent = _mockEvents.FirstOrDefault(e => e.Id == id);
            if (existingEvent == null)
            {
                return EventResult<EventDto>.FailureResult("Event not found");
            }

            // Update the event
            existingEvent.Title = request.Title;
            existingEvent.Description = request.Description;
            existingEvent.Category = request.Category;
            existingEvent.Location = request.Location;
            existingEvent.StartDate = request.StartDate;
            existingEvent.EndDate = request.EndDate;
            existingEvent.StartTime = request.StartTime;
            existingEvent.EndTime = request.EndTime;
            existingEvent.Price = request.Price;
            existingEvent.MaxAttendees = request.MaxAttendees;
            existingEvent.ImageUrl = request.ImageUrl;
            existingEvent.OrganizerName = request.OrganizerName;
            existingEvent.OrganizerEmail = request.OrganizerEmail;
            existingEvent.UpdatedAt = DateTime.UtcNow;

            return EventResult<EventDto>.SuccessResult(existingEvent, "Event updated successfully");
        }
        catch (Exception ex)
        {
            return EventResult<EventDto>.FailureResult(ex.Message);
        }
    }

    public async Task<EventResult<bool>> DeleteEventAsync(string id)
    {
        try
        {
            await Task.Delay(100); // Simulate async operation

            var eventItem = _mockEvents.FirstOrDefault(e => e.Id == id);
            if (eventItem == null)
            {
                return EventResult<bool>.FailureResult("Event not found");
            }

            _mockEvents.Remove(eventItem);
            return EventResult<bool>.SuccessResult(true, "Event deleted successfully");
        }
        catch (Exception ex)
        {
            return EventResult<bool>.FailureResult(ex.Message);
        }
    }
}
