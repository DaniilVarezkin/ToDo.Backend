using FluentValidation;

namespace ToDo.Application.TaskItems.Commands.PartialUpdateTaskItem
{
    public class PartialUpdateTaskItemCommandValidator : AbstractValidator<PartialUpdateTaskItemCommand>
    {
        public PartialUpdateTaskItemCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.UserId).NotEmpty();

            RuleFor(command => command.Title)
                .MaximumLength(250)
                .When(command => command.Title != null);

            RuleFor(command => command.Description)
                .MaximumLength(1000)
                .When(command => command.Description != null);
        }
    }
}
