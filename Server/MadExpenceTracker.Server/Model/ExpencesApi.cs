namespace MadExpenceTracker.Server.Model
{
   
    public class ExpencesApi
    {
        public Guid Id { get; set; }
        public required string RunningMonth { get; set; }
        public required IEnumerable<ExpenceApi> Expence { get; set; }
    }
}
