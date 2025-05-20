using AutoMapper;
using System.ComponentModel.DataAnnotations;
using ToDo.Application.Common.Mapping;
using ToDo.Application.TaskItems.Commands.UpdateTaskItem;
using ToDo.Domain.Enums;
using ToDo.Domain.Models;

namespace ToDo.WebApi.Models.TaskItems
{
    /// <summary>
    /// DTO для обновления задачи
    /// </summary>
    public class UpdateTaskItemDto : IMapped
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

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<UpdateTaskItemDto, UpdateTaskItemCommand>()
                .ForMember(cmd => cmd.Id, opt => opt.MapFrom(dto => dto.Id))
                .ForMember(cmd => cmd.UserId, opt => opt.MapFrom(dto => dto.UserId))
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
