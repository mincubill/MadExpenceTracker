namespace MadExpenceTracker.Server.Model
{
    public class AmountApi
    {
        public Guid Id { get; set; }
        public long TotalBaseExpences { get; set; }
        public long TotalAditionalExpences { get; set; }
        public long Savings { get; set; }
        public long TotalIncomes { get; set; }

    }
}
