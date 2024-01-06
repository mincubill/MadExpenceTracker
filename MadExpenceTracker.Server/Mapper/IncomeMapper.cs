using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Server.Model;

namespace MadExpenceTracker.Server.Mapper
{
    public static class IncomeMapper
    {
        public static IncomesApi MapToApi(Incomes input)
        {
            List<IncomeApi> expencesList = new List<IncomeApi>();
            foreach (var item in input.Income)
            {
                expencesList.Add(new IncomeApi
                {
                    Id = item.Id,
                    Name = item.Name,
                    Amount = item.Amount,
                    Date = item.Date,
                });
            }
            return new IncomesApi()
            {
                Id = input.Id,
                RunningMonth = input.RunningMonth,
                Income = expencesList
            };
        }

        public static IncomeApi MapToApi(Income input)
        {
            return new IncomeApi()
            {
                Id = input.Id,
                Name = input.Name,
                Amount = input.Amount,
                Date = input.Date
            };
        }

        public static IEnumerable<IncomesApi> MapToApi(IEnumerable<Incomes> input)
        {
            foreach (var income in input)
            {
                yield return new IncomesApi()
                {
                    Id = income.Id,
                    RunningMonth = income.RunningMonth,
                    Income = income.Income.Select(e => new IncomeApi
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Amount = e.Amount,
                        Date = e.Date
                    })
                };
            }
        }

        public static Income MapToModel(IncomeApi input)
        {
            return new Income()
            {
                Id = input.Id,
                Name = input.Name,
                Amount = input.Amount,
                Date = input.Date
            };
        }

        public static Incomes MapToModel(IncomesApi input)
        {
            return new Incomes()
            {
                Id = input.Id,
                RunningMonth = input.RunningMonth,
                Income = input.Income.Select(i => new Income
                {
                    Id = i.Id,
                    Name = i.Name,
                    Amount = i.Amount,
                    Date = i.Date
                })
            };
        }

    }
}
