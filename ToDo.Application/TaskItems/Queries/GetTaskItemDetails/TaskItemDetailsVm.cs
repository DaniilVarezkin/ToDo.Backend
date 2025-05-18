using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Common.Mapping;
using ToDo.Domain.Enums;

namespace ToDo.Application.TaskItems.Queries.GetTaskItemDetails
{
    public class TaskItemDetailsVm : IMapped
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime DueDate { get; set; }
        public required UserTaskStatus Status { get; set; }
        public required DateTime CreateDate { get; set; }
        public required DateTime UpdateDate { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            throw new NotImplementedException();
        }
    }
}
