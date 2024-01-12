namespace MadExpenceTracker.Server.Model
{
    public class MonthResumeApi
    {
        public required string MonthToClose { get; set; }
        public required Guid ExpencesId{ get; set; }
        public required Guid IncomesId { get; set; }
    }
}
