namespace MadExpenceTracker.Core.Model
{
    public class Amount
    {
        public Guid Id { get; set; }
        public long TotalBaseExpences { get; set; }
        public long SugestedBaseExpences { get; set; }
        public long RemainingBaseExpences { get; set; }
        public long TotalAditionalExpences { get; set; }
        public long SugestedAditionalExpences { get; set; }
        public long RemainingAditionalExpences { get; set; }
        public long SugestedSavings { get; set; }
        public long TotalSavings { get; set; }
        public long TotalIncomes { get; set; }

    }
}
