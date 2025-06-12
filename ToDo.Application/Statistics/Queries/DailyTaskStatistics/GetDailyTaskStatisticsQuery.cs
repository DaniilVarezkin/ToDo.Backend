using MediatR;
using ToDo.Shared.Dto.Statistics;

namespace ToDo.Application.Statistics.Queries.DailyTaskStatistics
{
    public class GetDailyTaskStatisticsQuery : IRequest<DailyTaskStatisticsVm>
    {
        public Guid UserId { get; set; }
        public int Days { get; set; }
    }
}
