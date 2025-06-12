using MediatR;
using ToDo.Application.TaskItems.Queries.Common;
using ToDo.Shared.Enums;

namespace ToDo.Application.TaskItems.Queries.GetTaskItemList
{
    /// <summary>
    /// Запрос на получение списка задач пользователя
    /// </summary>
    public class GetTaskItemListQuery : IRequest<PagedResult<TaskItemLookupDto>>
    {
        /// <summary>Идентификатор пользователя</summary>
        public Guid UserId { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        //Фильтрация
        public UserTaskStatus? Status { get; set; }
        public TaskPriority? Priority { get; set; }
        public bool? IsAllDay { get; set; }
        public DateTimeOffset? DateFrom { get; set; }
        public DateTimeOffset? DateTo { get; set; }
        public string? Search { get; set; }

        // Сортировка
        public string? SortBy { get; set; }      // например "StartDate" или "Priority"
        public bool SortDescending { get; set; } // по убыванию?
    }
}
