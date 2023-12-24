namespace MadExpenceTracker.Core.Model
{
    public class Income
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime Date { get; set; }
        public long Amount { get; set; }
    }
}
