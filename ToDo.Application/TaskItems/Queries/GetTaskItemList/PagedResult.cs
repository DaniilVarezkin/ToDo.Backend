using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.TaskItems.Queries.GetTaskItemList
{
    public class PagedResult<T>
    {
        public int Page {  get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public IReadOnlyList<T> Items { get; init; } = Array.Empty<T>();
    }
}
