using MediatR;

namespace ToDo.Application.Statistics.Queries.GlobalTaskStatistics
{
    public class GetTaskStatisticsQuery : IRequest<TaskStatisticsVm>
    {
        public Guid UserId { get; set; }
    }
}
