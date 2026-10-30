using FluentValidation;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Validators;

public class TaskEditDtoValidator : AbstractValidator<TaskEditDto>
{
    public TaskEditDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان الزامی است.")
            .MaximumLength(200).WithMessage("عنوان حداکثر ۲۰۰ کاراکتر باشد.");

        RuleFor(x => x.AssignedToId)
            .GreaterThan(0).WithMessage("انتخاب کاربر الزامی است.");
    }
}
