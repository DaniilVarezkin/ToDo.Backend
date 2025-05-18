using AutoMapper;
using ToDo.Application.Common.Mapping;
using ToDo.Domain.Enums;
using ToDo.Domain.Models;

namespace ToDo.Application.TaskItems.Queries.GetTaskItemList
{
    public class TaskItemLookupDto : IMapped
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime DueDate { get; set; }
        public required UserTaskStatus Status { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<TaskItem, TaskItemLookupDto>()
                .ForMember(vm => vm.Id, opt =>
                    opt.MapFrom(task => task.Id))
                .ForMember(vm => vm.Title, opt =>
                    opt.MapFrom(task => task.Title))
                .ForMember(vm => vm.Description, opt =>
                    opt.MapFrom(task => task.Description))
                .ForMember(vm => vm.DueDate, opt =>
                    opt.MapFrom(task => task.DueDate))
                .ForMember(vm => vm.Status, opt =>
                    opt.MapFrom(task => task.Status));
        }
    }
}