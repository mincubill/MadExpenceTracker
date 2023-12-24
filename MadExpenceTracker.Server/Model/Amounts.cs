namespace MadExpenceTracker.Server.Model
{
    public class Amounts
    {
        public string ObjectId { get; set; }
        public string RunningMonth { get; set; }
        public IEnumerable<Amount> Amount { get; set; }
    }
}
