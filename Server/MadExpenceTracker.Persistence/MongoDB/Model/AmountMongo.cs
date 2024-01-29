namespace MadExpenceTracker.Persistence.MongoDB.Model
{
    public class AmountMongo
    {
        public Guid Id { get; set; }
        public long TotalBaseExpences { get; set; }
        public long SugestedBaseExpences { get; set; }
        public long TotalAditionalExpences { get; set; }
        public long SugestedAditionalExpences { get; set; }
        public long Savings { get; set; }
        public long TotalSavings { get; set; }
        public long TotalIncomes { get; set; }
    }
}
