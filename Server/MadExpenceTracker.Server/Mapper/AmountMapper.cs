using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Server.Model;
using static MongoDB.Driver.WriteConcern;

namespace MadExpenceTracker.Server.Mapper
{
    public static class AmountMapper
    {
        public static AmountsApi MapToApi(Amounts input)
        {
            List<AmountApi> amountsList = new List<AmountApi>();
            foreach (var item in input.Amount)
            {
                amountsList.Add(new AmountApi
                {
                    Id = item.Id,
                    Savings = item.Savings,
                    TotalAditionalExpences = item.TotalAditionalExpences,
                    TotalBaseExpences = item.TotalBaseExpences,
                    TotalIncomes = item.TotalIncomes
                });
            }
            return new AmountsApi()
            {
                Id = input.Id,
                RunningMonth = input.RunningMonth,
                Amount = amountsList
            };
        }

        public static AmountApi MapToApi(Amount input)
        {
            return new AmountApi()
            {
                Id = input.Id,
                Savings = input.Savings,
                TotalAditionalExpences = input.TotalAditionalExpences,
                TotalBaseExpences = input.TotalBaseExpences,
                TotalIncomes = input.TotalIncomes
            };
        }

        public static Amount MapToModel(AmountApi input)
        {
            return new Amount()
            {
                Id = input.Id,
                Savings = input.Savings,
                TotalAditionalExpences = input.TotalAditionalExpences,
                TotalBaseExpences = input.TotalBaseExpences,
                TotalIncomes = input.TotalIncomes
            };
        }
    }
}
