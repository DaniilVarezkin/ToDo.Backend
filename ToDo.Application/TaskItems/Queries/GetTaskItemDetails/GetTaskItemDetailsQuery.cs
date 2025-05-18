using MediatR;

namespace ToDo.Application.TaskItems.Queries.GetTaskItemDetails
{
    public class GetTaskItemDetailsQuery : IRequest<TaskItemDetailsVm>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
