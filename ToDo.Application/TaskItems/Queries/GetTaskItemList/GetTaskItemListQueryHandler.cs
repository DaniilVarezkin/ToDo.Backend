using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Interfaces;
using ToDo.Shared.Dto.Common;
using ToDo.Shared.Dto.TaskItems;

namespace ToDo.Application.TaskItems.Queries.GetTaskItemList
{
    public class GetTaskItemListQueryHandler
        : IRequestHandler<GetTaskItemListQuery, PagedResult<TaskItemLookupDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetTaskItemListQueryHandler(IAppDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<PagedResult<TaskItemLookupDto>> Handle(GetTaskItemListQuery request, CancellationToken cancellationToken)
        {
            var baseQuery = _dbContext.TaskItems
                .Where(taskItem => taskItem.UserId == request.UserId);

            //Фильтрация
            if (request.Status.HasValue)
                baseQuery = baseQuery.Where(taskItem => taskItem.Status == request.Status.Value);
            if (request.IsAllDay.HasValue)
                baseQuery = baseQuery.Where(taskItem => taskItem.IsAllDay == request.IsAllDay.Value);
            if (request.Priority.HasValue)
                baseQuery = baseQuery.Where(taskItem => taskItem.Priority == request.Priority.Value);
            if (request.DateFrom.HasValue)
                baseQuery = baseQuery.Where(taskItem => taskItem.StartDate >= request.DateFrom.Value);
            if (request.DateTo.HasValue)
                baseQuery = baseQuery.Where(taskItem => taskItem.EndDate <= request.DateTo.Value);

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                baseQuery = baseQuery.Where(taskItem =>
                    taskItem.Title.Contains(request.Search) ||
                    (taskItem.Description ?? "").Contains(request.Search));
            }

            //Подсчёт кол-ва до пагинации
            var totalCount = await baseQuery.CountAsync(cancellationToken);

            baseQuery = (request.SortBy?.ToLower()) switch
            {
                "priority" => request.SortDescending
                                    ? baseQuery.OrderByDescending(taskItem => taskItem.Priority)
                                    : baseQuery.OrderBy(taskItem => taskItem.Priority),
                "status" => request.SortDescending
                                    ? baseQuery.OrderByDescending(taskItem => taskItem.Status)
                                    : baseQuery.OrderBy(taskItem => taskItem.Status),
                "startdate" => request.SortDescending
                                    ? baseQuery.OrderByDescending(taskItem => taskItem.StartDate)
                                    : baseQuery.OrderBy(taskItem => taskItem.StartDate),
                "enddate" => request.SortDescending
                                    ? baseQuery.OrderByDescending(taskItem => taskItem.EndDate)
                                    : baseQuery.OrderBy(taskItem => taskItem.EndDate),
                _ => baseQuery.OrderBy(taskItem => taskItem.CreateDate)
            }; //Поменять базу данных

            var taskItemsPaged = await baseQuery
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<TaskItemLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);


            return new PagedResult<TaskItemLookupDto>
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                Items = taskItemsPaged
            };
        }
    }
}
