using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Enums;

namespace ToDo.Application.Statistics.Queries.GlobalTaskStatistics
{
    /// <summary>
    /// Сводная статистика по задачам пользователя.
    /// </summary>
    public class TaskStatisticsVm
    {
        /// <summary>
        /// Общее число задач (всех статусов).
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Распределение задач по статусам.
        /// </summary>
        public Dictionary<UserTaskStatus, int> ByStatus { get; set; } = new();

        /// <summary>
        /// Среднее время выполнения задач (в часах).
        /// </summary>
        public double AvgCompletionTimeHours { get; set; }

        /// <summary>
        /// Количество просроченных (не выполненных) задач.
        /// </summary>
        public int OverdueCount { get; set; }
    }
}
