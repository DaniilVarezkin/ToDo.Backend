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

            // 3. Строим список дней, тоже в UTC+0
            var days = tasks
              .SelectMany(task =>
              {
                  // получаем диапазон дат в UTC
                  var fromDate = task.StartDate.UtcDateTime.Date;
                  var toDate = task.EndDate.UtcDateTime.Date;
                  return Enumerable.Range(0, (toDate - fromDate).Days + 1)
                      .Select(offset => new
                      {
                          // каждый день помечаем явно как UTC+0
                          Date = new DateTimeOffset(fromDate.AddDays(offset), TimeSpan.Zero),
                          Task = task
                      });
              })
              .GroupBy(x => x.Date)
              .OrderBy(g => g.Key)
              .Select(g => new CalendarDay
              {
                  Date = g.Key,
                  Tasks = g.Select(x => _mapper.Map<TaskItemLookupDto>(x.Task)).ToList()
              })
              .ToList();

            return new CalendarTaskItemsVm { Days = days };
        }
    }
}
