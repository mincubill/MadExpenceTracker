namespace MadExpenceTracker.Core.Model
{
    public class MonthIndex
    {
        public Guid Id { get; set; }
        public string Month { get; set; }
        public Guid ExpencesId { get; set; }
        public Guid IncomesId { get; set; }
        public Guid AmountsId { get; set; }
        public byte SavingsRate { get; set; }
    }
}
