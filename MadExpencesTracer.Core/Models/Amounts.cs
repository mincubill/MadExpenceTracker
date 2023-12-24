namespace MadExpenceTracker.Core.Model
{
    public class Amounts
    {
        public Guid Id { get; set; }
        public string RunningMonth { get; set; }
        public IEnumerable<Amount> Amount { get; set; }
    }
}
