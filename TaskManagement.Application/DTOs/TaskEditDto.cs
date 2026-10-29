namespace TaskManagement.Application.DTOs;

public class TaskEditDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AssignedToId { get; set; } = string.Empty;
}
