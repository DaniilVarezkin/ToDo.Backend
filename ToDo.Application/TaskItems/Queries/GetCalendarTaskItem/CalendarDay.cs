using ToDo.Application.TaskItems.Queries.Common;

namespace ToDo.Application.TaskItems.Queries.GetCalendarTaskItem
{
    /// <summary>
    /// Описание задач на конкретный день для календаря.
    /// </summary>
    public class CalendarDay
    {
        /// <summary>
        /// Дата (без времени) дня.
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Список DTO задач, попадающих на этот день.
        /// </summary>
        public IList<TaskItemLookupDto> Tasks { get; set; } = new List<TaskItemLookupDto>();
    }
}