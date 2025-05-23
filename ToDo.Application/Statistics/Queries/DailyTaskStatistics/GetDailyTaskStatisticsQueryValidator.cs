using FluentValidation;

namespace ToDo.Application.Statistics.Queries.DailyTaskStatistics
{
    public class GetDailyTaskStatisticsQueryValidator : AbstractValidator<GetDailyTaskStatisticsQuery>
    {
        public GetDailyTaskStatisticsQueryValidator()
        {
            RuleFor(query => query.UserId).NotEmpty();
            RuleFor(query => query.Days).GreaterThan(0);
        }
    }
}
