namespace MadExpenceTracker.Core.Model
{
    public class Expence
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime Date { get; set; }
        public ExpenceType ExpenceType { get; set; }
        public long Amount { get; set; }
    }
}
