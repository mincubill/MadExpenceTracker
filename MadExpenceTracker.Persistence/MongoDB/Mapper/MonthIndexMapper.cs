﻿using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Persistence.MongoDB.Model;

namespace MadExpenceTracker.Persistence.MongoDB.Mapper
{
    internal static class MonthIndexMapper
    {
        public static MonthIndexes MapToModel(MonthIndexesMongo input)
        {
            List<MonthIndex> indexesList = new List<MonthIndex>();
            foreach (var item in input.MonthIndex)
            {
                indexesList.Add(new MonthIndex
                {
                    SavingsRate = item.SavingsRate,
                    IncomesId = item.IncomesId,
                    ExpencesId = item.ExpencesId,
                    AmountsId = item.AmountsId,
                    Month = item.Month,
                    Id = item.Id
                });
            }
            return new MonthIndexes()
            {
                Id = input.Id,
                MonthIndex = indexesList
            };
        }

        public static MonthIndex MapToModel(MonthIndexMongo input)
        {
            return new MonthIndex()
            {
                SavingsRate = input.SavingsRate,
                IncomesId = input.IncomesId,
                ExpencesId = input.ExpencesId,
                AmountsId = input.AmountsId,
                Month = input.Month,
                Id = input.Id
            };
        }

        public static IEnumerable<MonthIndex> MapToModel(IEnumerable<MonthIndexMongo> input)
        {
            foreach (var item in input)
            {
                yield return new MonthIndex
                {
                    SavingsRate = item.SavingsRate,
                    IncomesId = item.IncomesId,
                    ExpencesId = item.ExpencesId,
                    AmountsId = item.AmountsId,
                    Month = item.Month,
                    Id = item.Id
                };
            }
        }

        public static IEnumerable<MonthIndexes> MapToModel(IEnumerable<MonthIndexesMongo> input)
        {
            foreach (var amounts in input)
            {
                yield return new MonthIndexes()
                {
                    Id = amounts.Id,
                    MonthIndex = amounts.MonthIndex.Select(m => new MonthIndex
                    {
                        SavingsRate = m.SavingsRate,
                        IncomesId = m.IncomesId,
                        ExpencesId = m.ExpencesId,
                        AmountsId = m.AmountsId,
                        Month = m.Month,
                        Id = m.Id
                    })
                };
            }
        }

        public static MonthIndexMongo MapToMongo(MonthIndex input)
        {
            return new MonthIndexMongo()
            {
                SavingsRate = input.SavingsRate,
                IncomesId = input.IncomesId,
                ExpencesId = input.ExpencesId,
                AmountsId = input.AmountsId,
                Month = input.Month,
                Id = input.Id
            };
        }
    }
}
