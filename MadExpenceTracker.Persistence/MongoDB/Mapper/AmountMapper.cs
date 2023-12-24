using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Persistence.MongoDB.Model;

namespace MadExpenceTracker.Persistence.MongoDB.Mapper
{
    internal static class AmountMapper
    {
        public static Amounts MapToModel(AmountsMongo input)
        {
            List<Amount> amountsList = new List<Amount>();
            foreach (var item in input.Amount)
            {
                amountsList.Add(new Amount
                {
                    Id = item.Id,
                    TotalIncomes = item.TotalIncomes,
                    TotalBaseExpences = item.TotalBaseExpences,
                    TotalAditionalExpences = item.TotalAditionalExpences,
                    Savings = item.Savings
                });
            }
            return new Amounts()
            {
                Id = input.Id,
                Amount = amountsList
            };
        }

        public static Amount MapToModel(AmountMongo input)
        {
            return new Amount()
            {
                Id = input.Id,
                TotalIncomes = input.TotalIncomes,
                TotalBaseExpences = input.TotalBaseExpences,
                TotalAditionalExpences = input.TotalAditionalExpences,
                Savings = input.Savings
            };
        }

        public static IEnumerable<Amount> MapToModel(IEnumerable<AmountMongo> input)
        {
            foreach (var item in input)
            {
                yield return new Amount
                {
                    Id = item.Id,
                    TotalIncomes = item.TotalIncomes,
                    TotalBaseExpences = item.TotalBaseExpences,
                    TotalAditionalExpences = item.TotalAditionalExpences,
                    Savings = item.Savings
                };
            }
        }

        public static IEnumerable<Amounts> MapToModel(IEnumerable<AmountsMongo> input)
        {
            foreach (var amounts in input)
            {
                yield return new Amounts()
                {
                    Id = amounts.Id,
                    Amount = amounts.Amount.Select(a => new Amount
                    {
                        Id = a.Id,
                        TotalIncomes = a.TotalIncomes,
                        TotalBaseExpences = a.TotalBaseExpences,
                        TotalAditionalExpences = a.TotalAditionalExpences,
                        Savings = a.Savings
                    })
                };
            }
        }

        public static AmountMongo MapToMongo(Amount input)
        {
            return new AmountMongo()
            {
                Id = input.Id,
                TotalIncomes = input.TotalIncomes,
                TotalBaseExpences = input.TotalBaseExpences,
                TotalAditionalExpences = input.TotalAditionalExpences,
                Savings = input.Savings
            };
        }
    }
}
