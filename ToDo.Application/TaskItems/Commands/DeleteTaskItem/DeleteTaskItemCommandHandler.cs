using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Common.Exceptions;
using ToDo.Application.Interfaces;
using ToDo.Domain.Models;

namespace ToDo.Application.TaskItems.Commands.DeleteTaskItem
{
    public class DeleteTaskItemCommandHandler
        : IRequestHandler<DeleteTaskItemCommand>
    {
        private readonly IAppDbContext _dbContext;
        public DeleteTaskItemCommandHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task Handle(DeleteTaskItemCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var taskItem = await _dbContext.TaskItems.FirstOrDefaultAsync(
                        task => (task.Id == request.Id) && (task.UserId == request.UserId),
                        cancellationToken);

                    if (taskItem == null)
                    {
                        throw new NotFoundException(nameof(TaskItem), request.Id);
                    }

                    _dbContext.TaskItems.Remove(taskItem);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }
    }
}
