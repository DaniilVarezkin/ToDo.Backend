using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Interfaces;

namespace ToDo.Application.TaskItems.Queries.GetTaskItemList
{
    public class GetTaskItemListQueryHandler
        : IRequestHandler<GetTaskItemListQuery, TaskItemListVm>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetTaskItemListQueryHandler(IAppDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<TaskItemListVm> Handle(GetTaskItemListQuery request, CancellationToken cancellationToken)
        {
            var taskItemsQuery = await _dbContext.TaskItems
                .Where(note => note.UserId == request.UserId)
                .ProjectTo<TaskItemLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new TaskItemListVm { TaskItems = taskItemsQuery };
        }
    }
}
