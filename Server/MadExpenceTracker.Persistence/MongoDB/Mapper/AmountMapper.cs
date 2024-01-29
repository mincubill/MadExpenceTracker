using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Persistence.MongoDB.Model;

namespace MadExpenceTracker.Persistence.MongoDB.Mapper
{
    internal static class AmountMapper
    {
        public static Amounts MapToModel(AmountsMongo input)
        {
            List<Amount> amountsList = new List<Amount>();
            if (input.Amount != null && input.Amount.Any())
            {
                foreach (var item in input.Amount)
                {
                    amountsList.Add(new Amount
                    {
                        Id = item.Id,
                        TotalIncomes = item.TotalIncomes,
                        TotalBaseExpences = item.TotalBaseExpences,
                        TotalAditionalExpences = item.TotalAditionalExpences,
                        SugestedSavings = item.Savings,
                        SugestedAditionalExpences = item.SugestedAditionalExpences,
                        SugestedBaseExpences = item.SugestedBaseExpences,
                        TotalSavings = item.TotalSavings,
                    });
                }
            }
                
            return new Amounts()
            {
                Id = input.Id,
                Amount = amountsList
            };
        }

        public static IEnumerable<Amounts> MapToModel(IEnumerable<AmountsMongo> input)
        {
            foreach (var amounts in input)
            {
                yield return new Amounts()
                {
                    Id = amounts.Id,
                    Amount = amounts.Amount != null ? amounts.Amount.Select(a => new Amount
                    {
                        Id = a.Id,
                        TotalIncomes = a.TotalIncomes,
                        TotalBaseExpences = a.TotalBaseExpences,
                        TotalAditionalExpences = a.TotalAditionalExpences,
                        SugestedSavings = a.Savings,
                        SugestedBaseExpences = a.SugestedBaseExpences,
                        SugestedAditionalExpences = a.SugestedAditionalExpences,
                        TotalSavings = a.TotalSavings,
                    }) : new List<Amount>()
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
                Savings = input.SugestedSavings,
                SugestedAditionalExpences = input.SugestedAditionalExpences,
                SugestedBaseExpences = input.SugestedBaseExpences,
                TotalSavings = input.TotalSavings,
            };
        }
    }
}
