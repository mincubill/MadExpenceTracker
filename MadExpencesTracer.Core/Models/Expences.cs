namespace MadExpenceTracker.Core.Model
{
    public class Expences
    {
        public Guid Id { get; set; }
        public required string RunningMonth { get; set; }
        public required IEnumerable<Expence> Expence { get; set; }
    }
}
