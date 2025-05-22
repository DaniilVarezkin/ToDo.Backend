using FluentValidation;

namespace ToDo.Application.TaskItems.Commands.CompleteTaskItem
{
    public class CompleteTaskItemCommandValidator : AbstractValidator<CompleteTaskItemCommand>
    {
        public CompleteTaskItemCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.UserId).NotEmpty();
        }
    }
}
