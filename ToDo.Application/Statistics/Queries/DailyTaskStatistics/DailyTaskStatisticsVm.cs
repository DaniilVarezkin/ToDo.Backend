namespace ToDo.Application.Statistics.Queries.DailyTaskStatistics
{
    public class DailyTaskStatisticsVm
    {
        public IList<DayStatisticItem> History { get; set; } = new List<DayStatisticItem>();
    }
}
