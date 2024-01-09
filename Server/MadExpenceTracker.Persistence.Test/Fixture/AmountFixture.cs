using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Persistence.MongoDB.Model;

namespace MadExpenceTracker.Persistence.Test.Fixture
{
    public static class AmountFixture
    {
        public static Amount GetAmount()
        {
            return new Amount
            {
                Id = Guid.Parse("df592bf9-ee3a-4a12-bd83-2c5817c150ed"),
                Savings = 200000,
                TotalAditionalExpences = 20,
                TotalBaseExpences = 100,
                TotalIncomes = 1000000,
                SugestedBaseExpences = 100,
                SugestedAditionalExpences = 100
            };
        }

        public static Amounts GetAmounts()
        {
            return new Amounts
            {
                Id = Guid.Parse("9c64e4d9-c9af-4714-8650-c98e6ebecfa7"),
                RunningMonth = "2023/12",
                Amount = new List<Amount>
                {
                    new Amount
                    {
                        Id = Guid.Parse("df592bf9-ee3a-4a12-bd83-2c5817c150ed"),
                        Savings = 200000,
                        TotalAditionalExpences = 20,
                        TotalBaseExpences = 100,
                        TotalIncomes = 1000000,
                        SugestedBaseExpences = 100,
                        SugestedAditionalExpences = 100
                    }
                }
            };
        }

        public static AmountMongo GetAmountMongo()
        {
            return new AmountMongo
            {
                Id = Guid.Parse("df592bf9-ee3a-4a12-bd83-2c5817c150ed"),
                Savings = 200000,
                TotalAditionalExpences = 20,
                TotalBaseExpences = 100,
                TotalIncomes = 1000000,
                SugestedBaseExpences = 100,
                SugestedAditionalExpences = 100
            };
        }

        public static AmountsMongo GetAmountsMongo()
        {
            return new AmountsMongo
            {
                ObjectId = "",
                Id = Guid.Parse("9c64e4d9-c9af-4714-8650-c98e6ebecfa7"),
                Amount = new List<AmountMongo>
                {
                    new AmountMongo
                    {
                        Id = Guid.Parse("df592bf9-ee3a-4a12-bd83-2c5817c150ed"),
                        Savings = 200000,
                        TotalAditionalExpences = 20,
                        TotalBaseExpences = 100,
                        TotalIncomes = 1000000,
                        SugestedBaseExpences = 100,
                        SugestedAditionalExpences = 100
                    }
                }
            };
        }
    }
}
