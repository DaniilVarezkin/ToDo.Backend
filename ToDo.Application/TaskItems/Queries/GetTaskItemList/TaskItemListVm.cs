namespace ToDo.Application.TaskItems.Queries.GetTaskItemList
{
    /// <summary>
    /// ViewModel списка задач
    /// </summary>
    public class TaskItemListVm
    {
        /// <summary>Список DTO задач</summary>
        public IList<TaskItemLookupDto> TaskItems { get; set; } = new List<TaskItemLookupDto>();
    }
}
