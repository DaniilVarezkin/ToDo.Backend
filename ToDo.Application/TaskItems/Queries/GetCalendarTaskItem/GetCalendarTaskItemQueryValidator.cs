using FluentValidation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ToDo.Application.TaskItems.Queries.GetCalendarTaskItem
{
    public class GetCalendarTaskItemQueryValidator : AbstractValidator<GetCalendarTaskItemQuery>
    {
        public GetCalendarTaskItemQueryValidator()
        {
            RuleFor(query => query.UserId).NotEmpty();
            RuleFor(query => query.StartDate).NotEmpty();
            RuleFor(query => query.EndDate).NotEmpty();

            RuleFor(query => query.StartDate)
                .LessThan(query => query.EndDate)
                .WithMessage("'StartDate' must be earlier than 'EndDate'.");

            RuleFor(query => query.EndDate)
                .GreaterThan(query => query.StartDate)
                .WithMessage("'EndDate' must be later than 'StartDate'.");
        }
    }
}
