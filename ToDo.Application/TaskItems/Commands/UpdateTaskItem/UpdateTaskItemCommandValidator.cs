using FluentValidation;

namespace ToDo.Application.TaskItems.Commands.UpdateTaskItem
{
    public class UpdateTaskItemCommandValidator : AbstractValidator<UpdateTaskItemCommand>
    {
        public UpdateTaskItemCommandValidator()
        {
            RuleFor(cmd => cmd.Id)
                .NotEmpty();

            RuleFor(cmd => cmd.UserId)
                .NotEmpty();

            RuleFor(cmd => cmd.Title)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(cmd => cmd.Description)
                .MaximumLength(1000);

            RuleFor(cmd => cmd.StartDate)
                .LessThan(cmd => cmd.EndDate)
                .WithMessage("'StartDate' must be earlier than 'EndDate'.");

            RuleFor(cmd => cmd.EndDate)
                .GreaterThan(cmd => cmd.StartDate)
                .WithMessage("'EndDate' must be later than 'StartDate'.");

            RuleFor(cmd => cmd.Color)
                .MaximumLength(20);

            RuleFor(cmd => cmd.RecurrenceRule)
                .NotEmpty()
                .When(cmd => cmd.IsRecurring)
                .WithMessage("'RecurrenceRule' must be specified when 'IsRecurring' is true.");
        }
    }
}
