using AutoMapper;
using ToDo.Application.TaskItems.Commands.CreateTaskItem;
using ToDo.Application.TaskItems.Commands.PartialUpdateTaskItem;
using ToDo.Application.TaskItems.Commands.UpdateTaskItem;
using ToDo.Application.TaskItems.Queries.GetTaskItemList;
using ToDo.Domain.Models;
using ToDo.Shared.Dto.TaskItems;

namespace ToDo.Application.Common.Mapping.MappingProfiles
{
    public class TaskItemMappingProfile : Profile
    {
        public TaskItemMappingProfile()
        {
            //TaskItem -> TaskItemLookupDto
            CreateMap<TaskItem, TaskItemLookupDto>()
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


            //TaskItem -> TaskItemDetailsVm
            CreateMap<TaskItem, TaskItemDetailsVm>()
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


            //Конфигурация маппинга CreateTaskItemDto -> CreateTaskItemCommand
            CreateMap<CreateTaskItemDto, CreateTaskItemCommand>()
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

            //Конфигурация маппинга UpdateTaskItemDto -> UpdateTaskItemCommand
            CreateMap<UpdateTaskItemDto, UpdateTaskItemCommand>()
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

            //Конфигурация маппинга PartialUpdateTaskItemDto -> PartialUpdateTaskItemCommand
            CreateMap<PartialUpdateTaskItemDto, PartialUpdateTaskItemCommand>()
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

            //Конфигурация маппинга GetTaskItemListQueryDto -> GetTaskItemListQuery
            CreateMap<GetTaskItemListQueryDto, GetTaskItemListQuery>()
                .ForMember(q => q.Page, opt => opt.MapFrom(dto => dto.Page))
                .ForMember(q => q.PageSize, opt => opt.MapFrom(dto => dto.PageSize))
                .ForMember(q => q.Status, opt => opt.MapFrom(dto => dto.Status))
                .ForMember(q => q.Priority, opt => opt.MapFrom(dto => dto.Priority))
                .ForMember(q => q.IsAllDay, opt => opt.MapFrom(dto => dto.IsAllDay))
                .ForMember(q => q.DateFrom, opt => opt.MapFrom(dto => dto.DateFrom))
                .ForMember(q => q.DateTo, opt => opt.MapFrom(dto => dto.DateTo))
                .ForMember(q => q.Search, opt => opt.MapFrom(dto => dto.Search))
                .ForMember(q => q.SortBy, opt => opt.MapFrom(dto => dto.SortBy))
                .ForMember(q => q.SortDescending, opt => opt.MapFrom(dto => dto.SortDescending));

        }
    }
}
