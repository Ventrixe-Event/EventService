using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities;

public class EventEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [Required]
    public string Category { get; set; } = null!;

    [Required]
    public string Location { get; set; } = null!;

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }

    [Required]
    public decimal Price { get; set; }

    public string Status { get; set; } = "Active";

    public int Progress { get; set; } = 0;

    public int MaxAttendees { get; set; } = 0;

    public int CurrentAttendees { get; set; } = 0;

    public string? ImageUrl { get; set; }

    public string? OrganizerName { get; set; }

    public string? OrganizerEmail { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;
}
