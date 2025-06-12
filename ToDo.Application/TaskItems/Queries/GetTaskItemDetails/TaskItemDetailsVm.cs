using AutoMapper;
using ToDo.Application.Common.Mapping;
using ToDo.Domain.Models;
using ToDo.Shared.Enums;

namespace ToDo.Application.TaskItems.Queries.GetTaskItemDetails
{
    /// <summary>
    /// ViewModel для детальной информации о задаче
    /// </summary>
    public class TaskItemDetailsVm : IMapped
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
        public DateTimeOffset UpdateDate { get; set; }
        public DateTimeOffset? CompletedDate { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<TaskItem, TaskItemDetailsVm>()
                .ForMember(vm => vm.Id, opt => opt.MapFrom(t => t.Id))
                .ForMember(vm => vm.Title, opt => opt.MapFrom(t => t.Title))
                .ForMember(vm => vm.Description, opt => opt.MapFrom(t => t.Description))
                .ForMember(vm => vm.IsAllDay, opt => opt.MapFrom(t => t.IsAllDay))
                .ForMember(vm => vm.StartDate, opt => opt.MapFrom(t => t.StartDate))
                .ForMember(vm => vm.EndDate, opt => opt.MapFrom(t => t.EndDate))
                .ForMember(vm => vm.Color, opt => opt.MapFrom(t => t.Color))
                .ForMember(vm => vm.IsRecurring, opt => opt.MapFrom(t => t.IsRecurring))
                .ForMember(vm => vm.RecurrenceRule, opt => opt.MapFrom(t => t.RecurrenceRule))
                .ForMember(vm => vm.Status, opt => opt.MapFrom(t => t.Status))
                .ForMember(vm => vm.Priority, opt => opt.MapFrom(t => t.Priority))
                .ForMember(vm => vm.CreateDate, opt => opt.MapFrom(t => t.CreateDate))
                .ForMember(vm => vm.UpdateDate, opt => opt.MapFrom(t => t.UpdateDate))
                .ForMember(vm => vm.CompletedDate, opt => opt.MapFrom(t => t.CompletedDate));
        }
    }
}
