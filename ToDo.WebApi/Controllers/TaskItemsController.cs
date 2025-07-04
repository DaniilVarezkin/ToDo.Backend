﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.TaskItems.Commands.CreateTaskItem;
using ToDo.Application.TaskItems.Commands.DeleteTaskItem;
using ToDo.Application.TaskItems.Commands.UpdateTaskItem;
using ToDo.Application.TaskItems.Queries.GetTaskItemDetails;
using ToDo.Application.TaskItems.Queries.GetTaskItemList;
using ToDo.WebApi.Models.TaskItems;

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
    [Route("api/[controller]/[action]")]
    public class TaskItemsController : BaseController
    {
        public readonly IMapper _mapper;
        public TaskItemsController(IMapper mapper) => _mapper = mapper;

        /// <summary xml:lang="en">
        /// Get the list of task items.
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получить список элементов задач.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET api/taskitems
        /// </remarks>
        /// <returns xml:lang="en">Returns TaskItemListVm.</returns>
        /// <returns xml:lang="ru">Возвращает TaskItemListVm.</returns>
        /// <response code="200" xml:lang="en">Success.</response>
        /// <response code="200" xml:lang="ru">Успешно.</response>
        /// <response code="401" xml:lang="en">If the user is unauthorized.</response>
        /// <response code="401" xml:lang="ru">Если пользователь не авторизован.</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TaskItemListVm>> GetAll()
        {
            var query = new GetTaskItemListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary xml:lang="en">
        /// Get the task item by id.
        /// </summary>
        /// <summary xml:lang="ru">
        /// Получить элемент задачи по идентификатору.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET api/taskitems/{id}
        /// </remarks>
        /// <param name="id" xml:lang="en">Task item id (GUID).</param>
        /// <param name="id" xml:lang="ru">Идентификатор элемента задачи (GUID).</param>
        /// <returns xml:lang="en">Returns TaskItemDetailsVm.</returns>
        /// <returns xml:lang="ru">Возвращает TaskItemDetailsVm.</returns>
        /// <response code="200" xml:lang="en">Success.</response>
        /// <response code="200" xml:lang="ru">Успешно.</response>
        /// <response code="401" xml:lang="en">If the user is unauthorized.</response>
        /// <response code="401" xml:lang="ru">Если пользователь не авторизован.</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        /// <summary xml:lang="en">
        /// Creates a new task item.
        /// </summary>
        /// <summary xml:lang="ru">
        /// Создает новый элемент задачи.
        /// </summary>
        /// <remarks>
        /// Sample request:
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
        /// <param name="createTaskItemDto" xml:lang="en">CreateTaskItemDto object.</param>
        /// <param name="createTaskItemDto" xml:lang="ru">Объект CreateTaskItemDto.</param>
        /// <returns xml:lang="en">Returns id (GUID).</returns>
        /// <returns xml:lang="ru">Возвращает идентификатор (GUID).</returns>
        /// <response code="200" xml:lang="en">Success.</response>
        /// <response code="200" xml:lang="ru">Успешно.</response>
        /// <response code="401" xml:lang="en">If the user is unauthorized.</response>
        /// <response code="401" xml:lang="ru">Если пользователь не авторизован.</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateTaskItemDto createTaskItemDto)
        {
            var command = _mapper.Map<CreateTaskItemCommand>(createTaskItemDto);
            command.UserId = UserId;

            var taskItemId = await Mediator.Send(command);
            return Ok(taskItemId);
        }

        /// <summary xml:lang="en">
        /// Updates an existing task item.
        /// </summary>
        /// <summary xml:lang="ru">
        /// Обновляет существующий элемент задачи.
        /// </summary>
        /// <remarks>
        /// Sample request:
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
        /// <param name="updateTaskItemDto" xml:lang="en">UpdateTaskItemDto object.</param>
        /// <param name="updateTaskItemDto" xml:lang="ru">Объект UpdateTaskItemDto.</param>
        /// <returns xml:lang="en">Returns NoContent.</returns>
        /// <returns xml:lang="ru">Не возвращает содержимого.</returns>
        /// <response code="204" xml:lang="en">Success.</response>
        /// <response code="204" xml:lang="ru">Успешно.</response>
        /// <response code="401" xml:lang="en">If the user is unauthorized.</response>
        /// <response code="401" xml:lang="ru">Если пользователь не авторизован.</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UpdateTaskItemDto updateTaskItemDto)
        {
            var command = _mapper.Map<UpdateTaskItemCommand>(updateTaskItemDto);
            command.UserId = UserId;

            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary xml:lang="en">
        /// Deletes the task item by id.
        /// </summary>
        /// <summary xml:lang="ru">
        /// Удаляет элемент задачи по идентификатору.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE api/taskitems/{id}
        /// </remarks>
        /// <param name="id" xml:lang="en">Task item id (GUID).</param>
        /// <param name="id" xml:lang="ru">Идентификатор элемента задачи (GUID).</param>
        /// <returns xml:lang="en">Returns NoContent.</returns>
        /// <returns xml:lang="ru">Не возвращает содержимого.</returns>
        /// <response code="204" xml:lang="en">Success.</response>
        /// <response code="204" xml:lang="ru">Успешно.</response>
        /// <response code="401" xml:lang="en">If the user is unauthorized.</response>
        /// <response code="401" xml:lang="ru">Если пользователь не авторизован.</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
