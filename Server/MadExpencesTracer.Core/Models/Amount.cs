﻿namespace MadExpenceTracker.Core.Model
{
    public class Amount
    {
        public Guid Id { get; set; }
        public long TotalBaseExpences { get; set; }
        public long SugestedBaseExpences { get; set; }
        public long TotalAditionalExpences { get; set; }
        public long SugestedAditionalExpences { get; set; }
        public long Savings { get; set; }
        public long TotalIncomes { get; set; }

    }
}
