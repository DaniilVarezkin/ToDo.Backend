using System;
using ToDo.Domain.Enums;

namespace ToDo.Domain.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime DueDate { get; set; }
        public required UserTaskStatus Status { get; set; }
        public required DateTime CreateDate { get; set; }
        public required DateTime UpdateDate { get; set; }
    }
}
