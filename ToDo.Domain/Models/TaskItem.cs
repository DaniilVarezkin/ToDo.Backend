using System.ComponentModel.DataAnnotations;
using ToDo.Domain.Enums;

namespace ToDo.Domain.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required, MaxLength(250)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public bool IsAllDay { get; set; } = false;

        // Начало и окончание задачи
        [Required]
        public DateTimeOffset StartDate { get; set; }

        [Required]
        public DateTimeOffset EndDate { get; set; }

        // Цвет отображения (hex или название)
        [MaxLength(20)]
        public string? Color { get; set; }

        // Повторяемость
        public bool IsRecurring { get; set; } = false;
        public string? RecurrenceRule { get; set; }    // iCal RRULE или кастомное представление

        public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdateDate { get; set; } = DateTimeOffset.UtcNow;

        public UserTaskStatus Status { get; set; } = UserTaskStatus.None;
        public DateTimeOffset? CompletedDate { get; set; }

        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    }


}
