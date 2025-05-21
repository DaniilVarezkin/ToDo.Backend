using MediatR;
using System.ComponentModel.DataAnnotations;
using ToDo.Domain.Enums;
using ToDo.Domain.Models;

namespace ToDo.Application.TaskItems.Commands.PartialUpdateTaskItem
{
    public class PartialUpdateTaskItemCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [MaxLength(250)]
        public string? Title { get; set; } = null;

        [MaxLength(1000)]
        public string? Description { get; set; } = null;

        public bool? IsAllDay { get; set; } = null;

        public DateTimeOffset? StartDate { get; set; } = null;

        public DateTimeOffset? EndDate { get; set; } = null;

        public string? Color { get; set; } = null;

        public bool? IsRecurring { get; set; } = null;

        public string? RecurrenceRule { get; set; } = null;

        public UserTaskStatus? Status { get; set; } = null;

        public TaskPriority? Priority { get; set; } = null;
    }
}
