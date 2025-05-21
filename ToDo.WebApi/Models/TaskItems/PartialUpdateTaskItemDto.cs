using AutoMapper;
using System.ComponentModel.DataAnnotations;
using ToDo.Application.Common.Mapping;
using ToDo.Application.TaskItems.Commands.PartialUpdateTaskItem;
using ToDo.Domain.Enums;
using ToDo.Domain.Models;

namespace ToDo.WebApi.Models.TaskItems
{
    /// <summary>
    /// DTO для частичного обновления элемента задачи.
    /// </summary>
    public class PartialUpdateTaskItemDto : IMapped
    {
        /// <summary>
        /// Идентификатор обновляемого элемента задачи.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Новое значение заголовка задачи (необязательно).
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Новое значение описания задачи (необязательно).
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Обновление флага «задача на весь день» (необязательно).
        /// </summary>
        public bool? IsAllDay { get; set; }

        /// <summary>
        /// Новое значение даты и времени начала задачи (необязательно).
        /// </summary>
        public DateTimeOffset? StartDate { get; set; }

        /// <summary>
        /// Новое значение даты и времени окончания задачи (необязательно).
        /// </summary>
        public DateTimeOffset? EndDate { get; set; }

        /// <summary>
        /// Обновление цвета отображения в формате HEX или названии цвета (необязательно).
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// Обновление флага повторяемости задачи (необязательно).
        /// </summary>
        public bool? IsRecurring { get; set; }

        /// <summary>
        /// Новое правило повторения в формате iCal RRULE (необязательно).
        /// </summary>
        public string? RecurrenceRule { get; set; }

        /// <summary>
        /// Обновление статуса задачи (необязательно).
        /// </summary>
        public UserTaskStatus? Status { get; set; }

        /// <summary>
        /// Обновление приоритета задачи (необязательно).
        /// </summary>
        public TaskPriority? Priority { get; set; }

        /// <summary>
        /// Настраивает маппинг AutoMapper из этого DTO в команду <see cref="PartialUpdateTaskItemCommand"/>.
        /// </summary>
        /// <param name="profile">Профиль AutoMapper для конфигурации.</param>
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<PartialUpdateTaskItemDto, PartialUpdateTaskItemCommand>()
                .ForMember(cmd => cmd.Id, opt => opt.MapFrom(dto => dto.Id))
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

