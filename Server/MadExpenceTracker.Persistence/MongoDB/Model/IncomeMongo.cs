namespace MadExpenceTracker.Persistence.MongoDB.Model
{
    public class IncomeMongo
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime Date { get; set; }
        public long Amount { get; set; }
    }
}
