using MediatR;
using ToDo.Application.Interfaces;
using ToDo.Domain.Models;

namespace ToDo.Application.TaskItems.Commands.CreateTaskItem
{
    public class CreateTaskItemCommandHandler
        : IRequestHandler<CreateTaskItemCommand, Guid>
    {
        private readonly IAppDbContext _dbContext;
        public CreateTaskItemCommandHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;
        public async Task<Guid> Handle(CreateTaskItemCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var taskItem = new TaskItem
                    {
                        UserId = request.UserId,
                        Title = request.Title,
                        Description = request.Description,
                        DueDate = request.DueDate,
                        Status = request.Status,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                    };

                    await _dbContext.TaskItems.AddAsync(taskItem, cancellationToken);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return taskItem.Id;
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
