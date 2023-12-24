namespace MadExpenceTracker.Persistence.MongoDB.Model
{
    internal class MonthIndexesMongo
    {
        public Guid Id { get; set; }
        public IEnumerable<MonthIndexMongo> MonthIndex { get; set; }
    }
}
