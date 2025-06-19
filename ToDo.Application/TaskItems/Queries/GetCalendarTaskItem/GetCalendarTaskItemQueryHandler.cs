using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Interfaces;
using ToDo.Domain.Models;
using ToDo.Shared.Dto.Common;
using ToDo.Shared.Dto.TaskItems;

namespace ToDo.Application.TaskItems.Queries.GetCalendarTaskItem
{
    public class GetCalendarTaskItemQueryHandler
        : IRequestHandler<GetCalendarTaskItemQuery, CalendarTaskItemsVm>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCalendarTaskItemQueryHandler(IAppDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<CalendarTaskItemsVm> Handle(GetCalendarTaskItemQuery request, CancellationToken cancellationToken)
        {
            // 1. Приводим границы к UTC+0
            var startUtc = new DateTimeOffset(request.StartDate.UtcDateTime, TimeSpan.Zero);
            var endUtc = new DateTimeOffset(request.EndDate.UtcDateTime, TimeSpan.Zero);

            // 2. Фильтруем по этим новым интервалам
            var tasks = await _dbContext.TaskItems
                .Where(x => x.UserId == request.UserId
                         && x.StartDate <= endUtc
                         && x.EndDate >= startUtc)
                .ToListAsync(cancellationToken);

            // 3. Строим список дней, корректно обрабатывая однодневные задачи
            var days = tasks
                .SelectMany(task =>
                {
                    var fromDate = task.StartDate.UtcDateTime.Date;

                    var toDate = task.EndDate.UtcDateTime;
                    if (toDate.TimeOfDay == TimeSpan.Zero)
                        toDate = toDate.AddTicks(-1); // исключаем ровно полночь

                    var toDateDate = toDate.Date;

                    return Enumerable.Range(0, (toDateDate - fromDate).Days + 1)
                        .Select(offset => new
                        {
                            Date = new DateTimeOffset(fromDate.AddDays(offset), TimeSpan.Zero),
                            Task = task
                        });
                })
                .GroupBy(x => x.Date)
                .OrderBy(g => g.Key)
                .Select(g => new CalendarDay
                {
                    Date = g.Key,
                    Tasks = g
                        .Select(x => x.Task)
                        .OrderBy(t => t.StartDate) // правильная сортировка внутри дня
                        .Select(task => _mapper.Map<TaskItemLookupDto>(task))
                        .ToList()
                })
                .ToList();

            return new CalendarTaskItemsVm { Days = days };
        }
    }
}
