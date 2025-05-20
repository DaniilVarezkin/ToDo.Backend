using AutoMapper;
using ToDo.Application.Common.Mapping;
using ToDo.Domain.Enums;
using ToDo.Domain.Models;

namespace ToDo.Application.TaskItems.Queries.GetTaskItemList
{
    /// <summary>
    /// DTO для списка задач
    /// </summary>
    public class TaskItemLookupDto : IMapped
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsAllDay { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string? Color { get; set; }
        public bool IsRecurring { get; set; }
        public string? RecurrenceRule { get; set; }
        public UserTaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset? CompletedDate { get; set; }


        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<TaskItem, TaskItemLookupDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.IsAllDay, opt => opt.MapFrom(src => src.IsAllDay))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.IsRecurring, opt => opt.MapFrom(src => src.IsRecurring))
                .ForMember(dest => dest.RecurrenceRule, opt => opt.MapFrom(src => src.RecurrenceRule))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
                .ForMember(dest => dest.CompletedDate, opt => opt.MapFrom(src => src.CompletedDate));
        }
    }
}