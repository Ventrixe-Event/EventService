namespace Application.Models;

public class EventDto
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Category { get; set; } = null!;
    public string Location { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; } = "Active";
    public int Progress { get; set; }
    public int MaxAttendees { get; set; }
    public int CurrentAttendees { get; set; }
    public string? ImageUrl { get; set; }
    public string? OrganizerName { get; set; }
    public string? OrganizerEmail { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }

    // Formatted properties for frontend consumption
    public string FormattedDate => StartDate.ToString("MMM dd, yyyy");
    public string FormattedTime => StartTime.ToString(@"h\:mm\ tt");
    public string FormattedDateAndTime => $"{FormattedDate} - {FormattedTime}";
}
