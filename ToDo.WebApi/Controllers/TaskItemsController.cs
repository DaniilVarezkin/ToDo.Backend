using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.TaskItems.Commands.CreateTaskItem;
using ToDo.Application.TaskItems.Commands.DeleteTaskItem;
using ToDo.Application.TaskItems.Commands.UpdateTaskItem;
using ToDo.Application.TaskItems.Queries.GetTaskItemDetails;
using ToDo.Application.TaskItems.Queries.GetTaskItemList;
using ToDo.WebApi.Models;

namespace ToDo.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TaskItemsController : BaseController
    {
        public readonly IMapper _mapper;
        public TaskItemsController(IMapper mapper) => _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<TaskItemListVm>> GetAll()
        {
            var query = new GetTaskItemListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
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

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromBody] CreateTaskItemDto createTaskItemDto)
        {
            var command = _mapper.Map<CreateTaskItemCommand>(createTaskItemDto);
            command.UserId = UserId;

            var noteId = await Mediator.Send(command);
            return Ok(noteId);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] UpdateTaskItemDto updateTaskItemDto)
        {
            var command = _mapper.Map<UpdateTaskItemCommand>(updateTaskItemDto);
            command.UserId = UserId;

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
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
