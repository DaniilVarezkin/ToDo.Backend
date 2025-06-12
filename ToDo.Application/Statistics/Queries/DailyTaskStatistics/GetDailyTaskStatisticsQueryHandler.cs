using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Interfaces;
using ToDo.Shared.Enums;

namespace ToDo.Application.Statistics.Queries.DailyTaskStatistics
{
    public class GetDailyTaskStatisticsQueryHandler
        : IRequestHandler<GetDailyTaskStatisticsQuery, DailyTaskStatisticsVm>
    {
        private readonly IAppDbContext _dbContext;

        public GetDailyTaskStatisticsQueryHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<DailyTaskStatisticsVm> Handle(GetDailyTaskStatisticsQuery request, CancellationToken cancellationToken)
        {
            var today = DateTimeOffset.UtcNow.Date;
            var fromDate = today.AddDays(-request.Days + 1);

            var rawStats = await _dbContext.TaskItems
                .Where(task =>
                    task.UserId == request.UserId &&
                    task.CompletedDate.HasValue &&
                    task.Status == UserTaskStatus.Done &&
                    task.CompletedDate.Value.Date >= fromDate &&
                    task.CompletedDate.Value.Date <= today)
                .GroupBy(task => task.CompletedDate!.Value.Date)
                .Select(group => new
                {
                    Day = group.Key,
                    Count = group.Count(),
                })
                .ToListAsync(cancellationToken);

            var history = Enumerable.Range(0, request.Days)
                .Select(offset =>
                {
                    var day = fromDate.AddDays(offset);
                    var stat = rawStats.FirstOrDefault(s => s.Day == day);
                    return new DayStatisticItem
                    {
                        Day = day,
                        CompletedCount = stat?.Count ?? 0
                    };
                })
                .ToList();

            return new DailyTaskStatisticsVm { History = history };
        }
    }
}
