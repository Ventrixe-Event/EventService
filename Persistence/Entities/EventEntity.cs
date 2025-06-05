/*
 * Event Entity - Data Model (EventEntity.cs)
 * ==========================================
 *
 * This entity represents the core event data structure for the Event Service.
 * It defines all the properties and constraints needed to manage events
 * within the Ventixe platform.
 *
 * Entity Features:
 * - String-based primary key using GUID for distributed system compatibility
 * - Data validation attributes for required fields and constraints
 * - Comprehensive event information including scheduling, pricing, and capacity
 * - Organizer contact information for event management
 * - Audit trail with creation and update timestamps
 * - Soft delete capability with IsActive flag
 *
 * Database Context:
 * - Currently used with mock data implementation
 * - Designed for Entity Framework Core integration
 * - Ready for migration to actual database when needed
 *
 * Development Notes:
 * - AI assistance provided by Claude 4 (Anthropic) for entity design,
 *   validation attributes, and property organization
 * - Follows Domain-Driven Design principles
 * - Optimized for JSON serialization to DTOs
 *
 * Author: Kim Hammerstad (with AI assistance from Claude 4)
 * Created: 2024
 */

using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities;

/// <summary>
/// Represents an event entity in the Ventixe event management system.
/// Contains all necessary information for event creation, management, and tracking.
/// </summary>
public class EventEntity
{
    /// <summary>
    /// Unique identifier for the event using GUID string format.
    /// Provides better distributed system compatibility than integer IDs.
    /// </summary>
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Event title/name - required field with descriptive name for the event.
    /// </summary>
    [Required]
    public string Title { get; set; } = null!;

    /// <summary>
    /// Optional detailed description of the event.
    /// Provides additional context and information for attendees.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Event category classification (e.g., "Music", "Technology", "Art & Design").
    /// Used for filtering and organizing events by type.
    /// </summary>
    [Required]
    public string Category { get; set; } = null!;

    /// <summary>
    /// Physical location where the event will take place.
    /// Should include venue name and address information.
    /// </summary>
    [Required]
    public string Location { get; set; } = null!;

    /// <summary>
    /// Start date of the event (date portion only).
    /// Combined with StartTime to determine exact event beginning.
    /// </summary>
    [Required]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Optional end date for multi-day events.
    /// If null, assumes single-day event using StartDate.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Start time of the event (time portion only).
    /// Combined with StartDate to determine exact event beginning.
    /// </summary>
    [Required]
    public TimeSpan StartTime { get; set; }

    /// <summary>
    /// Optional end time for the event.
    /// If null, assumes open-ended or unknown duration.
    /// </summary>
    public TimeSpan? EndTime { get; set; }

    /// <summary>
    /// Ticket price for the event in decimal format.
    /// Supports free events (price = 0) and paid events.
    /// </summary>
    [Required]
    public decimal Price { get; set; }

    /// <summary>
    /// Current status of the event (e.g., "Active", "Draft", "Cancelled").
    /// Defaults to "Active" for published events.
    /// </summary>
    public string Status { get; set; } = "Active";

    /// <summary>
    /// Progress percentage of event planning/preparation (0-100).
    /// Used for tracking event readiness and completion status.
    /// </summary>
    public int Progress { get; set; } = 0;

    /// <summary>
    /// Maximum number of attendees allowed for the event.
    /// 0 indicates unlimited capacity or capacity not specified.
    /// </summary>
    public int MaxAttendees { get; set; } = 0;

    /// <summary>
    /// Current number of registered/confirmed attendees.
    /// Used for capacity management and availability display.
    /// </summary>
    public int CurrentAttendees { get; set; } = 0;

    /// <summary>
    /// Optional URL to event image or poster.
    /// Used for visual representation in the frontend.
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Name of the event organizer or organizing entity.
    /// Optional contact information for event inquiries.
    /// </summary>
    public string? OrganizerName { get; set; }

    /// <summary>
    /// Email address of the event organizer.
    /// Used for contact and communication purposes.
    /// </summary>
    public string? OrganizerEmail { get; set; }

    /// <summary>
    /// Timestamp when the event record was created.
    /// Automatically set to UTC time for audit trail.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Timestamp when the event record was last updated.
    /// Automatically set to UTC time for audit trail.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Soft delete flag - indicates if the event is active or deleted.
    /// Defaults to true; set to false for soft deletion instead of hard delete.
    /// </summary>
    public bool IsActive { get; set; } = true;
}
