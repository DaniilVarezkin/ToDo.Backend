using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Common.Exceptions;
using ToDo.Application.Interfaces;
using ToDo.Domain.Models;
using ToDo.Shared.Enums;

namespace ToDo.Application.TaskItems.Commands.ReopenTaskItem
{
    public class ReopenTaskItemCommandHandler
        : IRequestHandler<ReopenTaskItemCommand>
    {
        private readonly IAppDbContext _dbContext;

        public ReopenTaskItemCommandHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task Handle(ReopenTaskItemCommand request, CancellationToken cancellationToken)
        {
            var taskItem = await _dbContext.TaskItems.FirstOrDefaultAsync(taskItem =>
                taskItem.Id == request.Id &&
                taskItem.UserId == request.UserId,
                cancellationToken);

            if (taskItem == null)
            {
                throw new NotFoundException(nameof(TaskItem), request.Id);
            }

            taskItem.CompletedDate = null;
            taskItem.Status = UserTaskStatus.Todo;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
