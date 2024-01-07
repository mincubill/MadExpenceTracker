namespace MadExpenceTracker.Persistence.MongoDB.Model
{
    public class ExpenceMongo
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime Date { get; set; }
        public required string ExpenceType { get; set; }
        public long Amount { get; set; }
    }
}
