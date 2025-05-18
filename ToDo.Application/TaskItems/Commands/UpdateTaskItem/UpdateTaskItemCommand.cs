using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Enums;

namespace ToDo.Application.TaskItems.Commands.UpdateTaskItem
{
    public class UpdateTaskItemCommand : IRequest
    {
        public required Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime DueDate { get; set; }
        public required UserTaskStatus Status { get; set; }
    }
}
