namespace ToDo.Domain.Models
{
    /// <summary>
    /// Перечисление приоритетов задач.
    /// </summary>
    public enum TaskPriority : byte
    {
        /// <summary>
        /// Приоритет не задан.
        /// </summary>
        None = 0,

        /// <summary>
        /// Низкий приоритет.
        /// </summary>
        Low,

        /// <summary>
        /// Средний приоритет.
        /// </summary>
        Medium,

        /// <summary>
        /// Высокий приоритет.
        /// </summary>
        High
    }
}
