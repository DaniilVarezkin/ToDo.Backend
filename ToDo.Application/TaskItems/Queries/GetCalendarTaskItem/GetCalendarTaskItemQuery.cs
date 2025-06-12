using MediatR;
using ToDo.Shared.Dto.TaskItems;

namespace ToDo.Application.TaskItems.Queries.GetCalendarTaskItem
{
    public class GetCalendarTaskItemQuery : IRequest<CalendarTaskItemsVm>
    {
        public Guid UserId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
