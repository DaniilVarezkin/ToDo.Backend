using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.TaskItems.Queries.GetTaskItemList
{
    public class GetTaskItemListQuery : IRequest<TaskItemListVm>
    {
        public Guid UserId { get; set; }
    }
}
