using AutoMapper;
using ToDo.Application.Common.Mapping;
using ToDo.Domain.Enums;

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
            throw new NotImplementedException();
        }
    }
}