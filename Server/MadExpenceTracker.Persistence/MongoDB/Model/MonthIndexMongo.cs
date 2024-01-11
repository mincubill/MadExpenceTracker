namespace MadExpenceTracker.Persistence.MongoDB.Model
{
    public class MonthIndexMongo
    {
        public Guid Id { get; set; }
        public string? Month { get; set; }
        public Guid ExpencesId { get; set; }
        public Guid IncomesId { get; set; }
        public Guid AmountsId { get; set; }
        public byte SavingsRate { get; set; }
        public byte BaseExpencesRate { get; set; }
        public byte AditionalExpencesRate { get; set; }
    }
}
