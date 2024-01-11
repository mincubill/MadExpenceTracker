namespace MadExpenceTracker.Server.Model
{
    public class AmountsApi
    {
        public Guid Id { get; set; }
        public IEnumerable<AmountApi>? Amount { get; set; }
    }
}
