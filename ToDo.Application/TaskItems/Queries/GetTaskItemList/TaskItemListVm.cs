namespace ToDo.Application.TaskItems.Queries.GetTaskItemList
{
    public class TaskItemListVm
    {
        public IList<TaskItemLookupDto> TaskItems { get; set; } = new List<TaskItemLookupDto>();
    }
}
