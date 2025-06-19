using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Common.Exceptions;
using ToDo.Application.Interfaces;
using ToDo.Domain.Models;

namespace ToDo.Application.TaskItems.Commands.UpdateTaskItem
{
    public class UpdateTaskItemCommandHandler : IRequestHandler<UpdateTaskItemCommand>
    {
        private readonly IAppDbContext _dbContext;

        public UpdateTaskItemCommandHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task Handle(UpdateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var taskItem = await _dbContext.TaskItems
                .FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId, cancellationToken);

            if (taskItem == null)
            {
                throw new NotFoundException(nameof(TaskItem), request.Id);
            }

            taskItem.Title = request.Title;
            taskItem.Description = request.Description;
            taskItem.IsAllDay = request.IsAllDay;
            taskItem.StartDate = request.StartDate.ToOffset(TimeSpan.Zero);
            taskItem.EndDate = request.EndDate.ToOffset(TimeSpan.Zero);
            taskItem.Color = request.Color;
            taskItem.IsRecurring = request.IsRecurring;
            taskItem.RecurrenceRule = request.RecurrenceRule;
            taskItem.Status = request.Status;
            taskItem.Priority = request.Priority;
            taskItem.UpdateDate = DateTimeOffset.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
