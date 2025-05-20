using FluentValidation;

namespace ToDo.Application.TaskItems.Commands.DeleteTaskItem
{
    public class DeleteTaskItemCommandValidator : AbstractValidator<DeleteTaskItemCommand>
    {
        public DeleteTaskItemCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.UserId).NotEmpty();
        }
    }
}
