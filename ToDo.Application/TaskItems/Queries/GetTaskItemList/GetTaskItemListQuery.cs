using MediatR;

namespace ToDo.Application.TaskItems.Queries.GetTaskItemList
{
    /// <summary>
    /// Запрос на получение списка задач пользователя
    /// </summary>
    public class GetTaskItemListQuery : IRequest<TaskItemListVm>
    {
        /// <summary>Идентификатор пользователя</summary>
        public Guid UserId { get; set; }
    }
}
