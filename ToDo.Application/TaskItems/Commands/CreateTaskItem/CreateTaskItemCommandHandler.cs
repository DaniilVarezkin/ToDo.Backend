using MediatR;
using ToDo.Application.Interfaces;
using ToDo.Domain.Models;

namespace ToDo.Application.TaskItems.Commands.CreateTaskItem
{
    public class CreateTaskItemCommandHandler : IRequestHandler<CreateTaskItemCommand, Guid>
    {
        private readonly IAppDbContext _dbContext;

        public CreateTaskItemCommandHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var taskItem = new TaskItem
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                IsAllDay = request.IsAllDay,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Color = request.Color,
                IsRecurring = request.IsRecurring,
                RecurrenceRule = request.RecurrenceRule,
                CreateDate = DateTimeOffset.UtcNow,
                UpdateDate = DateTimeOffset.UtcNow,
                Status = request.Status,
                Priority = request.Priority
            };

            _dbContext.TaskItems.Add(taskItem);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return taskItem.Id;
        }
    }
}
