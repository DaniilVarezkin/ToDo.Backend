using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Interfaces;
using ToDo.Application.Common.Exceptions;
using ToDo.Domain.Models;
using ToDo.Domain.Enums;

namespace ToDo.Application.TaskItems.Commands.CompleteTaskItem
{
    public class CompleteTaskItemCommandHandler
        : IRequestHandler<CompleteTaskItemCommand>
    {
        private readonly IAppDbContext _dbContext;

        public CompleteTaskItemCommandHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task Handle(CompleteTaskItemCommand request, CancellationToken cancellationToken)
        {
            var taskItem = await _dbContext.TaskItems.FirstOrDefaultAsync(taskItem =>
                taskItem.Id == request.Id && 
                taskItem.UserId == request.UserId, 
                cancellationToken);

            if (taskItem == null)
            {
                throw new NotFoundException(nameof(TaskItem), request.Id);
            }

            taskItem.CompletedDate = DateTime.UtcNow;
            taskItem.Status = UserTaskStatus.Done;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
