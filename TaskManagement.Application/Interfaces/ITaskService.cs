using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Interfaces;

public interface ITaskService
{
    Task<IReadOnlyList<TaskDto>> GetMyCreatedTasksAsync(int currentUserId);
    Task<TaskEditDto?> GetForEditAsync(int id, int currentUserId);
    Task<IReadOnlyList<UserOptionDto>> GetAssignableUsersAsync();
    Task CreateAsync(TaskCreateDto dto, int currentUserId);
    Task UpdateAsync(TaskEditDto dto, int currentUserId);
    Task DeleteAsync(int id, int currentUserId);
}
