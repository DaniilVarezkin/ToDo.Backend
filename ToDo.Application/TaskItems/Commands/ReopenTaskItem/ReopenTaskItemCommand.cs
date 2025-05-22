using MediatR;

namespace ToDo.Application.TaskItems.Commands.ReopenTaskItem
{
    public class ReopenTaskItemCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
