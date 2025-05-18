using MediatR;
using ToDo.Domain.Enums;

namespace ToDo.Application.TaskItems.Commands.CreateTaskItem
{
    public class CreateTaskItemCommand : IRequest<Guid>
    {
        public required Guid UserId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime DueDate { get; set; }
        public required UserTaskStatus Status { get; set; }
    }
}
