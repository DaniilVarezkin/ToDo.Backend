using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Common.Exceptions;
using ToDo.Application.Interfaces;
using ToDo.Domain.Models;

namespace ToDo.Application.TaskItems.Commands.PartialUpdateTaskItem
{
    public class PartialUpdateTaskItemCommandHandler
        : IRequestHandler<PartialUpdateTaskItemCommand>
    {
        private readonly IAppDbContext _dbContext;

        public PartialUpdateTaskItemCommandHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task Handle(PartialUpdateTaskItemCommand request, CancellationToken cancellationToken)
        {
            var taskItem = await _dbContext.TaskItems
                .FirstOrDefaultAsync(t => t.Id == request.Id && t.UserId == request.UserId, cancellationToken);

            if (taskItem == null)
            {
                throw new NotFoundException(nameof(TaskItem), request.Id);
            }

            if(request.Title != null) 
                taskItem.Title = request.Title;
            if(request.Description != null) 
                taskItem.Description = request.Description;
            if(request.IsAllDay.HasValue)
                taskItem.IsAllDay = request.IsAllDay.Value;
            if(request.StartDate.HasValue)
                taskItem.StartDate = request.StartDate.Value;
            if (request.EndDate.HasValue)
                taskItem.StartDate = request.EndDate.Value;
            if (request.Color != null)
                taskItem.Color = request.Color;
            if (request.IsRecurring.HasValue)
                taskItem.IsRecurring = request.IsRecurring.Value;
            if (request.RecurrenceRule != null)
                taskItem.RecurrenceRule = request.RecurrenceRule;
            if (request.Status.HasValue)
                taskItem.Status = request.Status.Value;
            if (request.Priority.HasValue)
                taskItem.Priority = request.Priority.Value;

            taskItem.UpdateDate = DateTimeOffset.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
