using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Interfaces;
using ToDo.Shared.Dto.Statistics;
using ToDo.Shared.Enums;

namespace ToDo.Application.Statistics.Queries.GlobalTaskStatistics
{
    public class GetTaskStatisticsQueryHandler
        : IRequestHandler<GetTaskStatisticsQuery, GlobalTaskStatisticsVm>
    {
        private readonly IAppDbContext _dbContext;
        public GetTaskStatisticsQueryHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<GlobalTaskStatisticsVm> Handle(GetTaskStatisticsQuery request, CancellationToken cancellationToken)
        {
            var userTasks = _dbContext.TaskItems
                .Where(taskItem => taskItem.UserId == request.UserId);

            var totalCount = await userTasks.CountAsync(cancellationToken);

            var byStatus = await userTasks
                .GroupBy(task => task.Status)
                .ToDictionaryAsync(group => group.Key, group => group.Count(),
                cancellationToken);

            var doneDates = await userTasks
                .Where(x => x.Status == UserTaskStatus.Done && x.CompletedDate.HasValue)
                .Select(x => new { x.StartDate, x.CompletedDate })
                .ToListAsync(cancellationToken);


            double avgCompletionTime = 0;
            if (doneDates.Any())
            {
                avgCompletionTime = doneDates
                    .Average(x => (x.CompletedDate!.Value - x.StartDate).TotalHours);
            }

            var overDueTaskCount = await userTasks.CountAsync(task =>
            task.Status != UserTaskStatus.Done &&
            task.EndDate < DateTimeOffset.UtcNow,
            cancellationToken);


            return new GlobalTaskStatisticsVm
            {
                TotalCount = totalCount,
                ByStatus = byStatus,
                AvgCompletionTimeHours = avgCompletionTime,
                OverdueCount = overDueTaskCount,
            };
        }
    }
}
