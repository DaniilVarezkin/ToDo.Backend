using FluentValidation;

namespace ToDo.Application.Statistics.Queries.GlobalTaskStatistics
{
    public class GetTaskStatisticsQueryValidator : AbstractValidator<GetTaskStatisticsQuery>
    {
        public GetTaskStatisticsQueryValidator()
        {
            RuleFor(query => query.UserId).NotEmpty();
        }
    }
}
