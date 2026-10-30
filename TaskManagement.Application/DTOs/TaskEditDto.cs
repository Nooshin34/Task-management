using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.DTOs;

public class TaskEditDto
{
    public int Id { get; set; }

    [Display(Name = "عنوان")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "توضیحات")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "اختصاص به")]
    public int AssignedToId { get; set; }
}
