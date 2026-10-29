using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Interfaces;

public interface ITaskService
{
    Task<IReadOnlyList<TaskDto>> GetMyCreatedTasksAsync(string currentUserId);
    Task<TaskEditDto?> GetForEditAsync(int id, string currentUserId);
    Task CreateAsync(TaskCreateDto dto, string currentUserId);
    Task UpdateAsync(TaskEditDto dto, string currentUserId);
    Task DeleteAsync(int id, string currentUserId);
}
