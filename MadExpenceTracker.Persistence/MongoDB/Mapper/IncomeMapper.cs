using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Persistence.MongoDB.Model;

namespace MasIncomeTracker.Persistence.MongoDB.Mapper
{
    internal static class IncomeMapper
    {
        public static Incomes MapToModel(IncomesMongo input)
        {
            List<Income> IncomesList = new List<Income>();
            foreach (var item in input.Incomes)
            {
                IncomesList.Add(new Income
                {
                    Id = item.Id,
                    Name = item.Name,
                    Amount = item.Amount,
                    Date = item.Date,
                });
            }
            return new Incomes()
            {
                Id = input.Id,
                RunningMonth = input.RunningMonth,
                Income = IncomesList
            };
        }

        public static Income MapToModel(IncomeMongo input)
        {
            return new Income()
            {
                Id = input.Id,
                Name = input.Name,
                Amount = input.Amount,
                Date = input.Date
            };
        }

        public static IEnumerable<Income> MapToModel(IEnumerable<IncomeMongo> input)
        {
            foreach (var item in input)
            {
                yield return new Income()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Amount = item.Amount,
                    Date = item.Date
                };
            }
        }

        public static IEnumerable<Incomes> MapToModel(IEnumerable<IncomesMongo> input)
        {
            foreach (var incomes in input)
            {
                yield return new Incomes()
                {
                    Id = incomes.Id,
                    RunningMonth = incomes.RunningMonth,
                    Income = incomes.Incomes.Select(e => new Income
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Amount = e.Amount,
                        Date = e.Date
                    })
                };
            }
        }

        public static IncomeMongo MapToMongo(Income input)
        {
            return new IncomeMongo()
            {
                Id = input.Id,
                Name = input.Name,
                Amount = input.Amount,
                Date = input.Date
            };
        }
    }
}
