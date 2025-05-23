namespace ToDo.Application.Statistics.Queries.DailyTaskStatistics
{
    public class DayStatisticItem
    {
        public DateTimeOffset Day { get; set; }
        public int CompletedCount { get; set; }
    }
}