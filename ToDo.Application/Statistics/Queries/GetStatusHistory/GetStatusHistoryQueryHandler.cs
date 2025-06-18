using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Interfaces;
using ToDo.Shared.Dto.Statistics;
using ToDo.Shared.Enums;

namespace ToDo.Application.Statistics.Queries.GetStatusHistory
{
    public class GetStatusHistoryQueryHandler
    : IRequestHandler<GetStatusHistoryQuery, TaskStatusHistoryVm>
    {
        private readonly IAppDbContext _db;
        public GetStatusHistoryQueryHandler(IAppDbContext db) => _db = db;

        public async Task<TaskStatusHistoryVm> Handle(
            GetStatusHistoryQuery request,
            CancellationToken ct)
        {
            var today = DateTimeOffset.UtcNow.Date;
            var fromDay = today.AddDays(-request.Days + 1);

            // 1) CreatedHistory ― задачи, у которых CreateDate попадает в диапазон
            var created = await _db.TaskItems
                .AsNoTracking()
                .Where(t => t.UserId == request.UserId
                            && t.CreateDate.Date >= fromDay
                            && t.CreateDate.Date <= today)
                .GroupBy(t => t.CreateDate.Date)
                .Select(g => new DayMetricVm { Day = g.Key, Count = g.Count() })
                .ToListAsync(ct);

            // 2) CompletedHistory ― задачи, у которых CompletedDate попадает в диапазон
            var completedRaw = await _db.TaskItems
                .AsNoTracking()
                .Where(t => t.UserId == request.UserId
                            && t.CompletedDate.HasValue
                            && t.CompletedDate.Value.Date >= fromDay
                            && t.CompletedDate.Value.Date <= today)
                .GroupBy(t => t.CompletedDate!.Value.Date)
                .Select(g => new DayMetricVm { Day = g.Key, Count = g.Count() })
                .ToListAsync(ct);

            // 3) OverdueHistory ― просроченные: EndDate в диапазоне и статус != Done
            var overdueRaw = await _db.TaskItems
                .AsNoTracking()
                .Where(t => t.UserId == request.UserId
                            && t.EndDate.Date >= fromDay
                            && t.EndDate.Date <= today
                            && t.Status != UserTaskStatus.Done)
                .GroupBy(t => t.EndDate.Date)
                .Select(g => new DayMetricVm { Day = g.Key, Count = g.Count() })
                .ToListAsync(ct);

            // 4) StatusHistory ― для каждого статуса сколько завершилось в день
            var statusRaw = await _db.TaskItems
                .AsNoTracking()
                .Where(t => t.UserId == request.UserId
                            && t.CompletedDate.HasValue
                            && t.CompletedDate.Value.Date >= fromDay
                            && t.CompletedDate.Value.Date <= today)
                .GroupBy(t => new { t.Status, Day = t.CompletedDate!.Value.Date })
                .Select(g => new {
                    g.Key.Status,
                    g.Key.Day,
                    Count = g.Count()
                })
                .ToListAsync(ct);

            // Вспомогательная инициализация пустых рядов
            List<DayMetricVm> InitEmpty() =>
                Enumerable.Range(0, request.Days)
                    .Select(o => new DayMetricVm
                    {
                        Day = fromDay.AddDays(o),
                        Count = 0
                    })
                    .ToList();

            // Собираем финальный VM
            var vm = new TaskStatusHistoryVm
            {
                CreatedHistory = InitEmpty(),
                CompletedHistory = InitEmpty(),
                OverdueHistory = InitEmpty(),
                StatusHistory = Enum.GetValues<UserTaskStatus>()
                                        .ToDictionary(s => s, _ => InitEmpty())
            };

            // Заполняем реальными числами
            void Fill(List<DayMetricVm> list, List<DayMetricVm> raw)
            {
                foreach (var x in raw)
                {
                    var e = list.First(y => y.Day.Date == x.Day.Date);
                    e.Count = x.Count;
                }
            }

            Fill(vm.CreatedHistory, created);
            Fill(vm.CompletedHistory, completedRaw);
            Fill(vm.OverdueHistory, overdueRaw);

            foreach (var g in statusRaw)
            {
                var list = vm.StatusHistory[g.Status];
                var entry = list.First(x => x.Day.Date == g.Day);
                entry.Count = g.Count;
            }

            return vm;
        }
    }

}
