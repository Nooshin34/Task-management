using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.DTOs;

public class TaskCreateDto
{
    [Display(Name = "عنوان")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "توضیحات")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "اختصاص به")]
    public int AssignedToId { get; set; }
}
