using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Statistics.Queries.DailyTaskStatistics
{
    public class GetDailyTaskStatisticsQuery : IRequest<DailyTaskStatisticsVm>
    {
        public Guid UserId { get; set; }
        public int Days { get; set; }
    }
}
