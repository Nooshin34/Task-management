namespace TaskManagement.Application.DTOs;

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string CreatorName { get; set; } = string.Empty;
    public string AssignedToName { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}
