namespace ToDo.Domain.Enums
{
    /// <summary>
    /// Перечисление статусов задачи пользователя.
    /// </summary>
    public enum UserTaskStatus : byte
    {
        /// <summary>
        /// Статус не задан.
        /// </summary>
        None = 0,

        /// <summary>
        /// Задача запланирована к выполнению.
        /// </summary>
        Todo,

        /// <summary>
        /// Задача находится в процессе выполнения.
        /// </summary>
        InProgress,

        /// <summary>
        /// Задача выполнена.
        /// </summary>
        Done,
    }
}
