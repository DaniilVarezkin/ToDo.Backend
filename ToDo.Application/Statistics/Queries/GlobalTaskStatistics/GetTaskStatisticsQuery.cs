using MediatR;
using ToDo.Shared.Dto.Statistics;

namespace ToDo.Application.Statistics.Queries.GlobalTaskStatistics
{
    public class GetTaskStatisticsQuery : IRequest<GlobalTaskStatisticsVm>
    {
        public Guid UserId { get; set; }
    }
}
