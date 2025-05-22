using MediatR;

namespace ToDo.Application.Statistics.Queries
{
    public class GetTaskStatisticsQuery : IRequest<TaskStatisticsVm>
    {
        public Guid UserId { get; set; }
    }
}
