using AutoMapper;
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
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class TaskItemsController : BaseController
    {
        public readonly IMapper _mapper;
        public TaskItemsController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Get the list of task items
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET api/taskitems
        /// </remarks>
        /// <returns>Returns TaskItemListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
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

        /// <summary>
        /// Get the task item by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET api/taskitems/F091F1EC-ED13-4D2D-BF4A-E340403D9531
        /// </remarks>
        /// <param name="id">Task item id (guid)</param>
        /// <returns>Returns TaskItemDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
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

        /// <summary>
        /// Creates a new task item
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
        /// <param name="createTaskItemDto">CreateTaskItemDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
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

        /// <summary>
        /// Updates an existing task item
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
        /// <param name="updateTaskItemDto">UpdateTaskItemDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
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

        /// <summary>
        /// Deletes the task item by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE api/taskitems/F435F181-DB34-409A-A19D-85A495E5C54C
        /// </remarks>
        /// <param name="id">Task item id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
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
