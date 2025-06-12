using System.ComponentModel.DataAnnotations;
using ToDo.Shared.Enums;

namespace ToDo.Domain.Models
{
    /// <summary>
    /// Сущность элемента задачи.
    /// </summary>
    public class TaskItem
    {
        /// <summary>
        /// Уникальный идентификатор задачи.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому принадлежит задача.
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// Заголовок задачи. Максимальная длина — 250 символов.
        /// </summary>
        [Required]
        [MaxLength(250)]
        public string Title { get; set; } = null!;

        /// <summary>
        /// Описание задачи. Максимальная длина — 1000 символов.
        /// </summary>
        [MaxLength(1000)]
        public string? Description { get; set; }

        /// <summary>
        /// Флаг, указывающий, что задача рассчитана на весь день.
        /// </summary>
        public bool IsAllDay { get; set; } = false;

        /// <summary>
        /// Дата и время начала задачи.
        /// </summary>
        [Required]
        public DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// Дата и время окончания задачи. Должно быть больше или равно StartDate.
        /// </summary>
        [Required]
        public DateTimeOffset EndDate { get; set; }

        /// <summary>
        /// Цвет отображения задачи в формате HEX или название цвета.
        /// Максимальная длина — 20 символов.
        /// </summary>
        [MaxLength(20)]
        public string? Color { get; set; }

        /// <summary>
        /// Флаг, указывающий, что задача повторяется.
        /// </summary>
        public bool IsRecurring { get; set; } = false;

        /// <summary>
        /// Правило повторения задачи в формате iCal RRULE или кастомное представление.
        /// </summary>
        public string? RecurrenceRule { get; set; }

        /// <summary>
        /// Дата и время создания задачи.
        /// </summary>
        public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Дата и время последнего обновления задачи.
        /// </summary>
        public DateTimeOffset UpdateDate { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Статус выполнения задачи.
        /// </summary>
        public UserTaskStatus Status { get; set; } = UserTaskStatus.None;

        /// <summary>
        /// Дата и время завершения задачи (если выполнено).
        /// </summary>
        public DateTimeOffset? CompletedDate { get; set; }

        /// <summary>
        /// Приоритет задачи.
        /// </summary>
        public TaskPriority Priority { get; set; } = TaskPriority.None;
    }
}
