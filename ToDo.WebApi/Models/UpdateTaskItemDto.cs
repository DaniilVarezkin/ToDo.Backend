using AutoMapper;
using ToDo.Application.Common.Mapping;
using ToDo.Application.TaskItems.Commands.CreateTaskItem;
using ToDo.Application.TaskItems.Commands.UpdateTaskItem;
using ToDo.Domain.Enums;

namespace ToDo.WebApi.Models
{
    public class UpdateTaskItemDto : IMapped
    {
        public required Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime DueDate { get; set; }
        public required UserTaskStatus Status { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<UpdateTaskItemDto, UpdateTaskItemCommand>()
                .ForMember(command => command.Id, opt =>
                    opt.MapFrom(dto => dto.Id))
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
