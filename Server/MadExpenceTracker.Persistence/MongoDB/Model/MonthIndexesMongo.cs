namespace MadExpenceTracker.Persistence.MongoDB.Model
{
    public class MonthIndexesMongo
    {
        public Guid Id { get; set; }
        public IEnumerable<MonthIndexMongo>? MonthIndex { get; set; }
    }
}
