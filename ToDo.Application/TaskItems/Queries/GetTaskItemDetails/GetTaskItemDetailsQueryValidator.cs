using FluentValidation;

namespace ToDo.Application.TaskItems.Queries.GetTaskItemDetails
{
    public class GetTaskItemDetailsQueryValidator : AbstractValidator<GetTaskItemDetailsQuery>
    {
        public GetTaskItemDetailsQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
            RuleFor(query => query.UserId).NotEmpty();
        }
    }
}
