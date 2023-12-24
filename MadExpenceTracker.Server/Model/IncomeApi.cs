namespace MadExpenceTracker.Server.Model
{
    public class IncomeApi
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime Date { get; set; }
        public long Amount { get; set; }
    }
}
