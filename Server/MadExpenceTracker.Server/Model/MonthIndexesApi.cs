namespace MadExpenceTracker.Server.Model
{
    public class MonthIndexesApi
    {
        public required Guid Id { get; set; }
        public required IEnumerable<MonthIndexApi> MonthIndex { get; set; }

    }
}
