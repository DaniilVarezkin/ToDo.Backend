using MediatR;
using ToDo.Shared.Dto.Statistics;
using ToDo.Shared.Enums;

namespace ToDo.Application.Statistics.Queries.GetStatusHistory
{
    public class GetStatusHistoryQuery : IRequest<TaskStatusHistoryVm>
    {
        public Guid UserId { get; set; }
        public int Days { get; set; }
    }
}
