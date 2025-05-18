using MediatR;

namespace ToDo.Application.TaskItems.Commands.DeleteTaskItem
{
    public class DeleteTaskItemCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
