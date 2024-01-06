namespace MadExpenceTracker.Server.Model
{
    public class MonthResumeApi
    {
        public required ExpencesApi ExpencesApi { get; set; }
        public required IncomesApi IncomesApi { get; set; }
        public required AmountApi AmountApi { get; set; }
    }
}
