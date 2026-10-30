using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;

    public TaskService(ITaskRepository taskRepository, IUserRepository userRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
    }

    public async Task<IReadOnlyList<TaskDto>> GetMyCreatedTasksAsync(int currentUserId)
    {
        var tasks = await _taskRepository.GetCreatedByUserAsync(currentUserId);

        return tasks.Select(t => new TaskDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            CreatedAt = t.CreatedAt,
            CreatorName = t.Creator?.DisplayName ?? string.Empty,
            AssignedToName = t.AssignedTo?.DisplayName ?? string.Empty,
            IsDeleted = t.IsDeleted
        }).ToList();
    }

    public async Task<IReadOnlyList<UserOptionDto>> GetAssignableUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(u => new UserOptionDto
        {
            Id = u.Id,
            FullName = string.IsNullOrWhiteSpace(u.DisplayName) ? u.Email : u.DisplayName
        }).ToList();
    }

    public async Task<TaskEditDto?> GetForEditAsync(int id, int currentUserId)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task is null || task.CreatorId != currentUserId || task.IsDeleted)
        {
            return null;
        }

        return new TaskEditDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            AssignedToId = task.AssignedToId
        };
    }

    public async Task CreateAsync(TaskCreateDto dto, int currentUserId)
    {
        var assignedUser = await _userRepository.GetByIdAsync(dto.AssignedToId);
        if (assignedUser is null)
        {
            throw new InvalidOperationException("کاربر انتخاب‌شده پیدا نشد.");
        }

        var task = new TaskItem
        {
            Title = dto.Title.Trim(),
            Description = dto.Description.Trim(),
            CreatedAt = DateTime.Now,
            CreatorId = currentUserId,
            AssignedToId = dto.AssignedToId,
            IsDeleted = false
        };

        await _taskRepository.AddAsync(task);
        await _taskRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskEditDto dto, int currentUserId)
    {
        var task = await _taskRepository.GetByIdAsync(dto.Id);
        if (task is null || task.CreatorId != currentUserId || task.IsDeleted)
        {
            throw new InvalidOperationException("اجازه ویرایش این تسک را ندارید.");
        }

        var assignedUser = await _userRepository.GetByIdAsync(dto.AssignedToId);
        if (assignedUser is null)
        {
            throw new InvalidOperationException("کاربر انتخاب‌شده پیدا نشد.");
        }

        task.Title = dto.Title.Trim();
        task.Description = dto.Description.Trim();
        task.AssignedToId = dto.AssignedToId;

        _taskRepository.Update(task);
        await _taskRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id, int currentUserId)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task is null || task.CreatorId != currentUserId || task.IsDeleted)
        {
            throw new InvalidOperationException("اجازه حذف این تسک را ندارید.");
        }

        task.IsDeleted = true;
        task.DeletedAt = DateTime.Now;

        _taskRepository.Update(task);
        await _taskRepository.SaveChangesAsync();
    }
}
