using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Web.Controllers;

[Authorize]
public class TasksController : Controller
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    private int CurrentUserId
    {
        get
        {
            var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(value, out var id))
            {
                throw new InvalidOperationException("کاربر وارد نشده است.");
            }

            return id;
        }
    }

    public async Task<IActionResult> Index()
    {
        var tasks = await _taskService.GetMyCreatedTasksAsync(CurrentUserId);
        return View(tasks);
    }

    public async Task<IActionResult> Create()
    {
        await FillUsersAsync();
        return View(new TaskCreateDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TaskCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            await FillUsersAsync();
            return View(dto);
        }

        try
        {
            await _taskService.CreateAsync(dto, CurrentUserId);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await FillUsersAsync();
            return View(dto);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var task = await _taskService.GetForEditAsync(id, CurrentUserId);
        if (task is null)
        {
            return NotFound();
        }

        await FillUsersAsync(task.AssignedToId);
        return View(task);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TaskEditDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            await FillUsersAsync(dto.AssignedToId);
            return View(dto);
        }

        try
        {
            await _taskService.UpdateAsync(dto, CurrentUserId);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await FillUsersAsync(dto.AssignedToId);
            return View(dto);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _taskService.DeleteAsync(id, CurrentUserId);
            return Json(new { success = true });
        }
        catch (InvalidOperationException)
        {
            return Json(new { success = false, message = "حذف این تسک ممکن نیست." });
        }
    }

    private async Task FillUsersAsync(int? selectedId = null)
    {
        var users = await _taskService.GetAssignableUsersAsync();
        ViewBag.Users = new SelectList(users, nameof(UserOptionDto.Id), nameof(UserOptionDto.FullName), selectedId);
    }
}
