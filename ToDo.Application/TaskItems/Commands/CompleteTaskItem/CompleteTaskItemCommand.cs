using MediatR;

namespace ToDo.Application.TaskItems.Commands.CompleteTaskItem
{
    public class CompleteTaskItemCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
