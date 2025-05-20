using AutoMapper;
using ToDo.Application.Common.Mapping;
using ToDo.Application.TaskItems.Commands.CreateTaskItem;
using ToDo.Domain.Enums;

namespace ToDo.WebApi.Models.TaskItems
{
    public class CreateTaskItemDto : IMapped
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime DueDate { get; set; }
        public required UserTaskStatus Status { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<CreateTaskItemDto, CreateTaskItemCommand>()
                .ForMember(command => command.Title, opt =>
                    opt.MapFrom(dto => dto.Title))
                .ForMember(command => command.Description, opt =>
                    opt.MapFrom(dto => dto.Description))
                .ForMember(command => command.DueDate, opt =>
                    opt.MapFrom(dto => dto.DueDate))
                .ForMember(command => command.Status, opt =>
                    opt.MapFrom(dto => dto.Status));
        }
    }
}
