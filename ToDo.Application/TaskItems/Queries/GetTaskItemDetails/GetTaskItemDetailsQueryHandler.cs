using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Common.Exceptions;
using ToDo.Application.Interfaces;
using ToDo.Domain.Models;
using ToDo.Shared.Dto.TaskItems;

namespace ToDo.Application.TaskItems.Queries.GetTaskItemDetails
{
    public class GetTaskItemDetailsQueryHandler
        : IRequestHandler<GetTaskItemDetailsQuery, TaskItemDetailsVm>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetTaskItemDetailsQueryHandler(IAppDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<TaskItemDetailsVm> Handle(GetTaskItemDetailsQuery request, CancellationToken cancellationToken)
        {
            var taskItem = await _dbContext.TaskItems.FirstOrDefaultAsync(
                        task => (task.Id == request.Id) && (task.UserId == request.UserId),
                        cancellationToken);

            if (taskItem == null)
            {
                throw new NotFoundException(nameof(TaskItem), request.Id);
            }

            return _mapper.Map<TaskItemDetailsVm>(taskItem);
        }
    }
}
