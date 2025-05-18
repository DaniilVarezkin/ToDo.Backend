using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Common.Exceptions;
using ToDo.Application.Interfaces;
using ToDo.Domain.Models;

namespace ToDo.Application.TaskItems.Commands.UpdateTaskItem
{
    public class UpdateTaskItemCommandHandler
        : IRequestHandler<UpdateTaskItemCommand>
    {
        private readonly IAppDbContext _dbContext;
        public UpdateTaskItemCommandHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task Handle(UpdateTaskItemCommand request, CancellationToken cancellationToken)
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

                    taskItem.Title = request.Title;
                    taskItem.Description = request.Description;
                    taskItem.Status = request.Status;
                    taskItem.DueDate = request.DueDate;
                    taskItem.UpdateDate = DateTime.Now;

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
