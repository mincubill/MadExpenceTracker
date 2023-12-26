namespace MadExpenceTracker.Server.Model
{
    public class MonthResumeApi
    {
        public ExpencesApi ExpencesApi { get; set; }
        public IncomesApi IncomesApi { get; set; }
        public AmountApi AmountApi { get; set; }
    }
}
