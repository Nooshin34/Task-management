using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(int id);
    Task<IReadOnlyList<TaskItem>> GetCreatedByUserAsync(string userId);
    Task AddAsync(TaskItem task);
    void Update(TaskItem task);
    Task SaveChangesAsync();
}
