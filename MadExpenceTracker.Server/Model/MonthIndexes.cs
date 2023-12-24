namespace MadExpenceTracker.Server.Model
{
    public class MonthIndexes
    {
        public required string ObjectId { get; set; }
        public required IEnumerable<MonthIndex> MonthIndex { get; set; }

    }
}
