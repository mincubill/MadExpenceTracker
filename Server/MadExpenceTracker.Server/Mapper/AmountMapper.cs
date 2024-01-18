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
            if(input.Amount != null && input.Amount.Any())
            {
                foreach (var item in input.Amount)
                {
                    amountsList.Add(new AmountApi
                    {
                        Id = item.Id,
                        Savings = item.Savings,
                        TotalAditionalExpences = item.TotalAditionalExpences,
                        TotalBaseExpences = item.TotalBaseExpences,
                        TotalIncomes = item.TotalIncomes,
                        SugestedAditionalExpences = item.SugestedAditionalExpences,
                        SugestedBaseExpences = item.SugestedBaseExpences,
                        RemainingAditionalExpences = item.RemainingAditionalExpences,
                        RemainingBaseExpences = item.RemainingBaseExpences,
                    });
                }
            }
            
            return new AmountsApi()
            {
                Id = input.Id,
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
                TotalIncomes = input.TotalIncomes,
                SugestedAditionalExpences = input.SugestedAditionalExpences,
                SugestedBaseExpences = input.SugestedBaseExpences,
                RemainingAditionalExpences = input.RemainingAditionalExpences,
                RemainingBaseExpences = input.RemainingBaseExpences,
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
                TotalIncomes = input.TotalIncomes,
                SugestedAditionalExpences = input.SugestedAditionalExpences,
                SugestedBaseExpences = input.SugestedBaseExpences,
                RemainingAditionalExpences = input.RemainingAditionalExpences,
                RemainingBaseExpences = input.RemainingBaseExpences,
            };
        }
    }
}
