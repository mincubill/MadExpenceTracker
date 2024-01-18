namespace MadExpenceTracker.Server.Model
{
    public class AmountApi
    {
        public Guid Id { get; set; }
        public long TotalBaseExpences { get; set; }
        public long SugestedBaseExpences { get; set; }
        public long RemainingBaseExpences { get; set; }
        public long TotalAditionalExpences { get; set; }
        public long SugestedAditionalExpences { get; set; }
        public long RemainingAditionalExpences { get; set; }
        public long Savings { get; set; }
        public long TotalIncomes { get; set; }

    }
}
