using MediatR;
using System.ComponentModel.DataAnnotations;
using ToDo.Domain.Enums;
using ToDo.Domain.Models;

namespace ToDo.Application.TaskItems.Commands.UpdateTaskItem
{
    public class UpdateTaskItemCommand : IRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required, MaxLength(250)]
        public string Title { get; set; } = null!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public bool IsAllDay { get; set; } = false;

        [Required]
        public DateTimeOffset StartDate { get; set; }

        [Required]
        public DateTimeOffset EndDate { get; set; }

        [MaxLength(20)]
        public string? Color { get; set; }

        public bool IsRecurring { get; set; } = false;

        public string? RecurrenceRule { get; set; }

        public UserTaskStatus Status { get; set; } = UserTaskStatus.None;

        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    }
}
