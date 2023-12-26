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
        public static IEnumerable<AmountsApi> MapToApi(IEnumerable<Amounts> input)
        {
            foreach (var amounts in input)
            {
                yield return new AmountsApi()
                {
                    Id = amounts.Id,
                    RunningMonth = amounts.RunningMonth,
                    Amount = amounts.Amount.Select(a => new AmountApi
                    {
                        Id = a.Id,
                        Savings = a.Savings,
                        TotalAditionalExpences = a.TotalAditionalExpences,
                        TotalBaseExpences = a.TotalBaseExpences,
                        TotalIncomes = a.TotalIncomes
                    })
                };
            }
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

        public static Amounts MapToModel(AmountsApi input)
        {
            return new Amounts()
            {
                Id = input.Id,
                RunningMonth = input.RunningMonth,
                Amount = input.Amount.Select(a => new Amount
                {
                    Id = a.Id,
                    Savings = a.Savings,
                    TotalAditionalExpences = a.TotalAditionalExpences,
                    TotalBaseExpences = a.TotalBaseExpences,
                    TotalIncomes = a.TotalIncomes
                })
            };
        }

        public static IEnumerable<Amount> MapToModel(IEnumerable<AmountApi> input)
        {
            foreach (var item in input)
            {
                yield return new Amount()
                {
                    Id = item.Id,
                    Savings = item.Savings,
                    TotalAditionalExpences = item.TotalAditionalExpences,
                    TotalBaseExpences = item.TotalBaseExpences,
                    TotalIncomes = item.TotalIncomes
                };
            }
        }

        public static IEnumerable<Amounts> MapToModel(IEnumerable<AmountsApi> input)
        {
            foreach (var amount in input)
            {
                yield return new Amounts()
                {
                    Id = amount.Id,
                    RunningMonth = amount.RunningMonth,
                    Amount = amount.Amount.Select(a => new Amount
                    {
                        Id = a.Id,
                        Savings = a.Savings,
                        TotalAditionalExpences = a.TotalAditionalExpences,
                        TotalBaseExpences = a.TotalBaseExpences,
                        TotalIncomes = a.TotalIncomes
                    })
                };
            }
        }
    }
}
