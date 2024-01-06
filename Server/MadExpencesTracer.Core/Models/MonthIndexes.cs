namespace MadExpenceTracker.Core.Model
{
    public class MonthIndexes
    {
        public Guid Id { get; set; }
        public required IEnumerable<MonthIndex> MonthIndex { get; set; }

    }
}
