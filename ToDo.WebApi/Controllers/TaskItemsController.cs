using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.TaskItems.Commands.CompleteTaskItem;
using ToDo.Application.TaskItems.Commands.CreateTaskItem;
using ToDo.Application.TaskItems.Commands.DeleteTaskItem;
using ToDo.Application.TaskItems.Commands.PartialUpdateTaskItem;
using ToDo.Application.TaskItems.Commands.ReopenTaskItem;
using ToDo.Application.TaskItems.Commands.UpdateTaskItem;
using ToDo.Application.TaskItems.Queries.GetCalendarTaskItem;
using ToDo.Application.TaskItems.Queries.GetTaskItemDetails;
using ToDo.Application.TaskItems.Queries.GetTaskItemList;
using ToDo.Shared.Dto.Common;
using ToDo.Shared.Dto.TaskItems;

namespace ToDo.WebApi.Controllers
{
    /// <summary>
    /// Контроллер задач.
    /// </summary>
    /// <remarks>
    /// Предоставляет методы для получения, создания, обновления и удаления
    /// задач текущего авторизованного пользователя.
    /// </remarks>
    [ApiController]
    [Route("api/task-items/[action]")]
    public class TaskItemsController : BaseController
    {
        public readonly IMapper _mapper;
        public TaskItemsController(IMapper mapper) => _mapper = mapper;

        /// <summary xml:lang="ru">
        /// Получить список элементов задач с фильтрацией, сортировкой и пагинацией.
        /// </summary>
        /// <remarks xml:lang="en">
        /// Пример запроса:
        /// GET api/taskitems?page=1&amp;pageSize=10&amp;status=Done&amp;priority=High&amp;search=meeting
        /// </remarks>
        /// <param name="queryDto" xml:lang="ru">
        /// Параметры запроса для фильтрации (Status, Priority, IsAllDay, DateFrom/DateTo, Search), сортировки (SortBy, SortDescending) и пагинации (Page, PageSize).
        /// </param>
        /// <returns xml:lang="ru">
        /// Возвращает постраничный список элементов задач (TaskItemListVm).
        /// </returns>
        /// <response code="200" xml:lang="ru">Успешно.</response>
        /// <response code="401" xml:lang="ru">Если пользователь не авторизован.</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(PagedResult<TaskItemLookupDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PagedResult<TaskItemLookupDto>>> GetAll(
            [FromQuery] GetTaskItemListQueryDto queryDto)
        {
            var query = _mapper.Map<GetTaskItemListQuery>(queryDto);
            query.UserId = UserId;
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary xml:lang="ru">
        /// Получить элемент задачи по идентификатору.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET api/taskitems/{id}
        /// </remarks>
        /// <param name="id" xml:lang="ru">Идентификатор элемента задачи (GUID).</param>
        /// <returns xml:lang="ru">Возвращает TaskItemDetailsVm.</returns>
        /// <response code="200" xml:lang="ru">Успешно.</response>
        /// <response code="401" xml:lang="ru">Если пользователь не авторизован.</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(TaskItemDetailsVm), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TaskItemDetailsVm>> Get(Guid id)
        {
            var query = new GetTaskItemDetailsQuery
            {
                UserId = UserId,
                Id = id
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary xml:lang="ru">
        /// Получить элементы задач для календаря в заданном диапазоне дат.
        /// </summary>
        /// <remarks xml:lang="en">
        /// Пример запроса:
        /// GET api/taskitems/calendar?start=2025-05-01T00:00:00+02:00&amp;end=2025-05-31T23:59:59+02:00
        /// </remarks>
        /// <param name="start" xml:lang="ru">
        /// Включающая начальная дата/время диапазона для получения элементов календаря.
        /// </param>
        /// <param name="end" xml:lang="ru">
        /// Включающая конечная дата/время диапазона для получения элементов календаря.
        /// </param>
        /// <returns xml:lang="ru">
        /// Возвращает объект CalendarTaskItemsVm с элементами задач для отображения в календаре.
        /// </returns>
        /// <response code="200" xml:lang="ru">Успешно.</response>
        /// <response code="400" xml:lang="ru">Некорректные данные.</response>
        /// <response code="401" xml:lang="ru">Если пользователь не авторизован.</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(CalendarTaskItemsVm), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CalendarTaskItemsVm>> Calendar(
            [FromQuery] DateTimeOffset start,
            [FromQuery] DateTimeOffset end)
        {
            var query = new GetCalendarTaskItemQuery
            {
                UserId = UserId,
                StartDate = start,
                EndDate = end
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary xml:lang="ru">
        /// Создает новый элемент задачи.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST api/taskitems
        /// {
        ///     "title": "Task title",
        ///     "description": "Task description",
        ///     "isAllDay": false,
        ///     "startDate": "2025-05-20T09:00:00+02:00",
        ///     "endDate": "2025-05-20T17:00:00+02:00",
        ///     "color": "#FF0000",
        ///     "isRecurring": false,
        ///     "recurrenceRule": null,
        ///     "status": 0,
        ///     "priority": 2
        /// }
        /// </remarks>
        /// <param name="createTaskItemDto" xml:lang="ru">Объект CreateTaskItemDto.</param>
        /// <returns xml:lang="ru">Возвращает идентификатор (GUID).</returns>
        /// <response code="201" xml:lang="ru">Успешное создание задачи.</response>
        /// <response code="400" xml:lang="ru">Некорректные данные для создания.</response>
        /// <response code="401" xml:lang="ru">Если пользователь не авторизован.</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateTaskItemDto createTaskItemDto)
        {
            var command = _mapper.Map<CreateTaskItemCommand>(createTaskItemDto);
            command.UserId = UserId;

            var taskItemId = await Mediator.Send(command);
            return CreatedAtAction(
                nameof(Get),
                new { id = taskItemId },
                taskItemId);
        }

        /// <summary xml:lang="ru">
        /// Отмечает элемент задачи как выполненный.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST api/taskitems/complete/{id}
        /// </remarks>
        /// <param name="id" xml:lang="ru">Идентификатор элемента задачи (GUID).</param>
        /// <response code="204" xml:lang="ru">Не возвращает содержимого.</response>
        /// <response code="401" xml:lang="ru">Если пользователь не авторизован.</response>
        /// <response code="404" xml:lang="ru">Если задача по указанному id не была найдена.</response>
        [HttpPost("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Complete(Guid id)
        {
            var command = new CompleteTaskItemCommand
            {
                Id = id,
                UserId = UserId
            };

            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary xml:lang="ru">
        /// Снимает отметку о выполнении задачи и переводит её в статус Todo.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST api/taskitems/reopen/{id}
        /// </remarks>
        /// <param name="id" xml:lang="ru">Идентификатор элемента задачи (GUID).</param>
        /// <response code="204" xml:lang="ru">Не возвращает содержимого.</response>
        /// <response code="401" xml:lang="ru">Если пользователь не авторизован.</response>
        /// <response code="404" xml:lang="ru">Если задача по указанному id не была найдена.</response>
        [HttpPost("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Reopen(Guid id)
        {
            var command = new ReopenTaskItemCommand
            {
                Id = id,
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary xml:lang="ru">
        /// Обновляет существующий элемент задачи.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT api/taskitems
        /// {
        ///     "id": "FFC6D4FB-45D0-4B5A-BC56-8450A36C5547",
        ///     "title": "Updated task title",
        ///     "description": "Updated description",
        ///     "isAllDay": false,
        ///     "startDate": "2025-05-21T09:00:00+02:00",
        ///     "endDate": "2025-05-21T17:00:00+02:00",
        ///     "color": "#00FF00",
        ///     "isRecurring": false,
        ///     "recurrenceRule": null,
        ///     "status": 1,
        ///     "priority": 1
        /// }
        /// </remarks>
        /// <param name="updateTaskItemDto" xml:lang="ru">Объект UpdateTaskItemDto.</param>
        /// <returns xml:lang="ru">Не возвращает содержимого.</returns>
        /// <response code="204" xml:lang="ru">Успешно.</response>
        /// <response code="400" xml:lang="ru">Некорректные данные для обновления.</response>
        /// <response code="401" xml:lang="ru">Если пользователь не авторизован.</response>
        /// <response code="404" xml:lang="ru">Если задача по указанному id не была найдена.</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateTaskItemDto updateTaskItemDto)
        {
            var command = _mapper.Map<UpdateTaskItemCommand>(updateTaskItemDto);
            command.UserId = UserId;

            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary xml:lang="ru">
        /// Частично обновляет элемент задачи.
        /// </summary>
        /// <remarks xml:lang="en">
        /// Пример запроса:
        /// PATCH api/taskitems/partialupdate
        /// {
        ///     "id": "F091F1EC-ED13-4D2D-BF4A-E340403D9531",
        ///     "status": 2,
        ///     "completedDate": "2025-05-21T14:00:00Z"
        /// }
        /// </remarks>
        /// <param name="partialUpdateTaskItemDto" xml:lang="ru">DTO с полями для обновления.</param>
        /// <response code="204" xml:lang="ru">Нет содержимого.</response>
        /// <response code="400" xml:lang="ru">Неверный запрос при ошибке валидации.</response>
        /// <response code="401" xml:lang="ru">Если пользователь не аутентифицирован.</response>
        /// <response code="404" xml:lang="ru">Если задача по указанному id не была найдена.</response>
        [HttpPatch]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PartialUpdate([FromBody] PartialUpdateTaskItemDto partialUpdateTaskItemDto)
        {
            var command = _mapper.Map<PartialUpdateTaskItemCommand>(partialUpdateTaskItemDto);
            command.UserId = UserId;

            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary xml:lang="ru">
        /// Удаляет элемент задачи по идентификатору.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE api/taskitems/{id}
        /// </remarks>
        /// <param name="id" xml:lang="ru">Идентификатор элемента задачи (GUID).</param>
        /// <returns xml:lang="ru">Не возвращает содержимого.</returns>
        /// <response code="204" xml:lang="ru">Успешно.</response>
        /// <response code="401" xml:lang="ru">Если пользователь не авторизован.</response>
        /// <response code="404" xml:lang="ru">Если задача по указанному id не была найдена.</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteTaskItemCommand
            {
                Id = id,
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
