namespace TaskManagement.Domain.Entities;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public string CreatorId { get; set; } = string.Empty;
    public ApplicationUser? Creator { get; set; }

    public string AssignedToId { get; set; } = string.Empty;
    public ApplicationUser? AssignedTo { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
