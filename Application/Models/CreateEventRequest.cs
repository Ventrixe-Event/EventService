using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class CreateEventRequest
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = null!;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Required]
    [StringLength(100)]
    public string Category { get; set; } = null!;

    [Required]
    [StringLength(500)]
    public string Location { get; set; } = null!;

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Max attendees must be a positive number")]
    public int MaxAttendees { get; set; } = 0;

    public string? ImageUrl { get; set; }

    public string? OrganizerName { get; set; }

    [EmailAddress]
    public string? OrganizerEmail { get; set; }
}
