/*
 * Event Data Transfer Object - API Response Model (EventDto.cs)
 * ============================================================
 *
 * This DTO represents the event data structure sent to frontend clients.
 * It mirrors the EventEntity but includes additional computed properties
 * for enhanced frontend user experience.
 *
 * DTO Features:
 * - Direct mapping from EventEntity for data consistency
 * - Computed formatting properties for date/time display
 * - Optimized for JSON serialization with camelCase naming
 * - Contains all necessary event information for frontend display
 *
 * Computed Properties:
 * - FormattedDate: Human-readable date format (MMM dd, yyyy)
 * - FormattedTime: Human-readable time format (h:mm tt)
 * - FormattedDateAndTime: Combined date and time string
 *
 * Development Notes:
 * - AI assistance provided by Claude 4 (Anthropic) for DTO design,
 *   property organization, and computed property implementation
 * - Designed for efficient serialization to JSON for API responses
 * - Includes formatting logic to reduce frontend processing
 *
 * Author: Kim Hammerstad (with AI assistance from Claude 4)
 * Created: 2024
 */

namespace Application.Models;

/// <summary>
/// Data Transfer Object representing an event for API responses.
/// Contains all event information along with computed formatting properties
/// for enhanced frontend display capabilities.
/// </summary>
public class EventDto
{
    /// <summary>
    /// Unique identifier for the event using GUID string format.
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Event title/name for display purposes.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Optional detailed description of the event.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Event category classification for filtering and organization.
    /// </summary>
    public string Category { get; set; } = null!;

    /// <summary>
    /// Physical location where the event will take place.
    /// </summary>
    public string Location { get; set; } = null!;

    /// <summary>
    /// Start date of the event (date portion only).
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Optional end date for multi-day events.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Start time of the event (time portion only).
    /// </summary>
    public TimeSpan StartTime { get; set; }

    /// <summary>
    /// Optional end time for the event.
    /// </summary>
    public TimeSpan? EndTime { get; set; }

    /// <summary>
    /// Ticket price for the event in decimal format.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Current status of the event (e.g., "Active", "Draft", "Cancelled").
    /// </summary>
    public string Status { get; set; } = "Active";

    /// <summary>
    /// Progress percentage of event planning/preparation (0-100).
    /// </summary>
    public int Progress { get; set; }

    /// <summary>
    /// Maximum number of attendees allowed for the event.
    /// </summary>
    public int MaxAttendees { get; set; }

    /// <summary>
    /// Current number of registered/confirmed attendees.
    /// </summary>
    public int CurrentAttendees { get; set; }

    /// <summary>
    /// Optional URL to event image or poster.
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Name of the event organizer or organizing entity.
    /// </summary>
    public string? OrganizerName { get; set; }

    /// <summary>
    /// Email address of the event organizer.
    /// </summary>
    public string? OrganizerEmail { get; set; }

    /// <summary>
    /// Timestamp when the event record was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when the event record was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Soft delete flag - indicates if the event is active or deleted.
    /// </summary>
    public bool IsActive { get; set; }

    // === Computed Properties for Frontend Formatting ===

    /// <summary>
    /// Formatted date string for display purposes.
    /// Returns the start date in "MMM dd, yyyy" format (e.g., "Jan 15, 2024").
    /// Computed property to reduce frontend formatting logic.
    /// </summary>
    public string FormattedDate => StartDate.ToString("MMM dd, yyyy");

    /// <summary>
    /// Formatted time string for display purposes.
    /// Returns the start time in "h:mm tt" format (e.g., "2:30 PM").
    /// Computed property to reduce frontend formatting logic.
    /// </summary>
    public string FormattedTime => DateTime.Today.Add(StartTime).ToString("h:mm tt");

    /// <summary>
    /// Combined formatted date and time string for display purposes.
    /// Returns a formatted string combining date and time (e.g., "Jan 15, 2024 - 2:30 PM").
    /// Computed property to provide ready-to-display datetime information.
    /// </summary>
    public string FormattedDateAndTime => $"{FormattedDate} - {FormattedTime}";
}
