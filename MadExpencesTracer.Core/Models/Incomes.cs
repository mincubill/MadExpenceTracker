namespace MadExpenceTracker.Core.Model
{
    public class Incomes
    {
        public Guid Id { get; set; }
        public required string RunningMonth { get; set; }
        public required IEnumerable<Income> Income { get; set; }
    }
}
