﻿using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Persistence.MongoDB.Model;

namespace MasIncomeTracker.Persistence.MongoDB.Mapper
{
    internal static class IncomeMapper
    {
        public static Incomes MapToModel(IncomesMongo input)
        {
            List<Income> IncomesList = new List<Income>();
            if(input.Incomes != null && input.Incomes.Any())
            {
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
            }
            return new Incomes()
            {
                Id = input.Id,
                RunningMonth = input.RunningMonth ?? string.Empty,
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

        public static IEnumerable<Incomes> MapToModel(IEnumerable<IncomesMongo> input)
        {
            foreach (var incomes in input)
            {
                yield return new Incomes()
                {
                    Id = incomes.Id,
                    RunningMonth = incomes.RunningMonth ?? string.Empty,
                    Income = incomes.Incomes != null ? incomes.Incomes.Select(e => new Income
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Amount = e.Amount,
                        Date = e.Date
                    }) : new List<Income>()
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
