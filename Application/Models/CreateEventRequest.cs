/*
 * Create Event Request - Input Model (CreateEventRequest.cs)
 * =========================================================
 *
 * This model defines the input structure for creating and updating events
 * in the Ventixe platform. It includes comprehensive validation attributes
 * to ensure data integrity and provide meaningful error messages.
 *
 * Validation Features:
 * - Required field validation for essential event information
 * - String length limits to prevent database overflow
 * - Email format validation for organizer contact
 * - Range validation for numeric values (price, attendees)
 * - Comprehensive error messages for user feedback
 *
 * Model Usage:
 * - Used for both POST (create) and PUT (update) operations
 * - Validated by ASP.NET Core model binding and validation
 * - Provides consistent input format for business logic layer
 *
 * Development Notes:
 * - AI assistance provided by Claude 4 (Anthropic) for validation design,
 *   attribute configuration, and error message optimization
 * - Follows ASP.NET Core validation best practices
 * - Designed for JSON deserialization from frontend requests
 *
 * Author: Kim Hammerstad (with AI assistance from Claude 4)
 * Created: 2024
 */

using System.ComponentModel.DataAnnotations;

namespace Application.Models;

/// <summary>
/// Input model for creating and updating events in the Ventixe platform.
/// Contains validation attributes to ensure data integrity and provide
/// meaningful error messages for invalid input.
/// </summary>
public class CreateEventRequest
{
    /// <summary>
    /// Event title/name - required field with maximum 200 characters.
    /// Should be descriptive and engaging for potential attendees.
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// Optional detailed description of the event.
    /// Limited to 1000 characters to provide sufficient detail while maintaining readability.
    /// </summary>
    [StringLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Event category classification - required field with maximum 100 characters.
    /// Used for organizing and filtering events (e.g., "Music", "Technology", "Art & Design").
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Category { get; set; } = null!;

    /// <summary>
    /// Physical location where the event will take place - required field.
    /// Should include venue name and address, limited to 500 characters.
    /// </summary>
    [Required]
    [StringLength(500)]
    public string Location { get; set; } = null!;

    /// <summary>
    /// Start date of the event - required field.
    /// Should be a future date for new events (validation can be added in business logic).
    /// </summary>
    [Required]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Optional end date for multi-day events.
    /// If provided, should be on or after the start date (validation can be added in business logic).
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Start time of the event - required field.
    /// Represents the time portion of when the event begins.
    /// </summary>
    [Required]
    public TimeSpan StartTime { get; set; }

    /// <summary>
    /// Optional end time for the event.
    /// If provided, should be after the start time (validation can be added in business logic).
    /// </summary>
    public TimeSpan? EndTime { get; set; }

    /// <summary>
    /// Ticket price for the event - required field with positive value validation.
    /// Supports free events (price = 0) and paid events.
    /// Uses decimal type for precise monetary calculations.
    /// </summary>
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
    public decimal Price { get; set; }

    /// <summary>
    /// Maximum number of attendees allowed for the event.
    /// Defaults to 0 indicating unlimited capacity or capacity not specified.
    /// Must be a positive number if specified.
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Max attendees must be a positive number")]
    public int MaxAttendees { get; set; } = 0;

    /// <summary>
    /// Optional URL to event image or poster.
    /// Used for visual representation in the frontend.
    /// No length restriction as URLs can vary significantly.
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Optional name of the event organizer or organizing entity.
    /// Used for contact and credibility purposes.
    /// </summary>
    public string? OrganizerName { get; set; }

    /// <summary>
    /// Optional email address of the event organizer.
    /// Includes email format validation for data integrity.
    /// Used for contact and communication purposes.
    /// </summary>
    [EmailAddress]
    public string? OrganizerEmail { get; set; }
}
