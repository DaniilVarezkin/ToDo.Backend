using ToDo.Application.TaskItems.Queries.Common;
using ToDo.Application.TaskItems.Queries.GetTaskItemList;

namespace ToDo.Application.TaskItems.Queries.GetCalendarTaskItem
{
    /// <summary>
    /// Модель представления для календарного вида задач.
    /// </summary>
    public class CalendarTaskItemsVm
    {
        /// <summary>
        /// Список дней с задачами.
        /// </summary>
        public IList<CalendarDay> Days { get; set; } = new List<CalendarDay>();
    }
}
