using AutoMapper;
using System.ComponentModel.DataAnnotations;
using ToDo.Application.Common.Mapping;
using ToDo.Application.TaskItems.Commands.CreateTaskItem;
using ToDo.Domain.Enums;
using ToDo.Domain.Models;

namespace ToDo.WebApi.Models.TaskItems
{
    /// <summary>
    /// DTO для создания нового элемента задачи.
    /// </summary>
    public class CreateTaskItemDto : IMapped
    {
        /// <summary>
        /// Заголовок элемента задачи. Максимальная длина — 250 символов.
        /// </summary>
        [Required]
        [MaxLength(250)]
        public string Title { get; set; } = null!;

        /// <summary>
        /// Дополнительное описание элемента задачи. Максимальная длина — 1000 символов.
        /// </summary>
        [MaxLength(1000)]
        public string? Description { get; set; }

        /// <summary>
        /// Флаг, указывающий, что задача занимает весь день.
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
        /// Цвет отображения в формате HEX (например, "#FF0000").
        /// </summary>
        [MaxLength(20)]
        public string? Color { get; set; }

        /// <summary>
        /// Флаг, указывающий на повторяемость задачи.
        /// </summary>
        public bool IsRecurring { get; set; } = false;

        /// <summary>
        /// Правило повторения в формате iCal RRULE (применяется, если IsRecurring = true).
        /// </summary>
        public string? RecurrenceRule { get; set; }

        /// <summary>
        /// Текущий статус задачи.
        /// </summary>
        public UserTaskStatus Status { get; set; } = UserTaskStatus.None;

        /// <summary>
        /// Приоритет задачи.
        /// </summary>
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        /// <summary>
        /// Настраивает маппинг AutoMapper от DTO к команде CreateTaskItemCommand.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper для конфигурации.</param>
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<CreateTaskItemDto, CreateTaskItemCommand>()
                .ForMember(cmd => cmd.Title, opt => opt.MapFrom(dto => dto.Title))
                .ForMember(cmd => cmd.Description, opt => opt.MapFrom(dto => dto.Description))
                .ForMember(cmd => cmd.IsAllDay, opt => opt.MapFrom(dto => dto.IsAllDay))
                .ForMember(cmd => cmd.StartDate, opt => opt.MapFrom(dto => dto.StartDate))
                .ForMember(cmd => cmd.EndDate, opt => opt.MapFrom(dto => dto.EndDate))
                .ForMember(cmd => cmd.Color, opt => opt.MapFrom(dto => dto.Color))
                .ForMember(cmd => cmd.IsRecurring, opt => opt.MapFrom(dto => dto.IsRecurring))
                .ForMember(cmd => cmd.RecurrenceRule, opt => opt.MapFrom(dto => dto.RecurrenceRule))
                .ForMember(cmd => cmd.Status, opt => opt.MapFrom(dto => dto.Status))
                .ForMember(cmd => cmd.Priority, opt => opt.MapFrom(dto => dto.Priority));
        }
    }
}
