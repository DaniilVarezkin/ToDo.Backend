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
            var tasks = await _dbContext.TaskItems
                .Where(x => x.UserId == request.UserId
                         && x.StartDate <= request.EndDate
                         && x.EndDate >= request.StartDate)
                .ToListAsync(cancellationToken);

            var days = tasks
              .SelectMany(task =>
              {
                  var from = task.StartDate.Date;
                  var to = task.EndDate.Date;
                  var list = new List<(DateTimeOffset date, TaskItem task)>();
                  for (var day = from; day <= to; day = day.AddDays(1))
                      list.Add((day, task));
                  return list;
              })
              .GroupBy(pair => pair.date)
              .OrderBy(group => group.Key)
              .Select(group => new CalendarDay
              {
                  Date = group.Key,
                  Tasks = group
                    .Select(pair => _mapper.Map<TaskItemLookupDto>(pair.task))
                    .ToList()
              })
              .ToList();

            return new CalendarTaskItemsVm { Days = days };
        }
    }
}
