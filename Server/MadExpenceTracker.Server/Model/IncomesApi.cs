namespace MadExpenceTracker.Server.Model
{
    public class IncomesApi
    {
        public required Guid Id { get; set; }
        public required string RunningMonth { get; set; }
        public required IEnumerable<IncomeApi> Income { get; set; }
    }
}
