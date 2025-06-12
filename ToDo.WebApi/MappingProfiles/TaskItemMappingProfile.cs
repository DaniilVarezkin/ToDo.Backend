using AutoMapper;
using ToDo.Application.TaskItems.Commands.CreateTaskItem;
using ToDo.Application.TaskItems.Commands.PartialUpdateTaskItem;
using ToDo.Application.TaskItems.Commands.UpdateTaskItem;
using ToDo.Application.TaskItems.Queries.GetTaskItemList;
using ToDo.Shared.Dto.TaskItems;

namespace ToDo.WebApi.MappingProfiles
{
    public class TaskItemMappingProfile : Profile
    {
        /// <summary>
        /// Добавляет конфигурацию маппинга для TaskItems DTO 
        /// </summary>
        public TaskItemMappingProfile()
        {
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
