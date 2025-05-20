using FluentValidation;

namespace ToDo.Application.TaskItems.Queries.GetTaskItemList
{
    public class GetTaskItemListQueryValidator : AbstractValidator<GetTaskItemListQuery>
    {
        public GetTaskItemListQueryValidator()
        {
            RuleFor(query => query.UserId).NotEmpty();
        }
    }
}
