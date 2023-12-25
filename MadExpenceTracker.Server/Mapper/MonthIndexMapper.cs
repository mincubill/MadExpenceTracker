using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Server.Model;

namespace MadExpenceTracker.Server.Mapper
{
    public static class MonthIndexMapper
    {
        public static MonthIndexApi MapToApi(MonthIndex input)
        {
            return new MonthIndexApi()
            {
                Id = input.Id,
                SavingsRate = input.SavingsRate,
                AmountsId = input.AmountsId,
                ExpencesId = input.ExpencesId,
                IncomesId = input.IncomesId,
                Month = input.Month
            };
        }

        public static MonthIndexesApi MapToApi(MonthIndexes input)
        {
            List<MonthIndexApi> indexesList = new List<MonthIndexApi>();
            foreach (var item in input.MonthIndex)
            {
                indexesList.Add(new MonthIndexApi
                {
                    Id = item.Id,
                    SavingsRate = item.SavingsRate,
                    AmountsId = item.AmountsId,
                    ExpencesId = item.ExpencesId,
                    IncomesId = item.IncomesId,
                    Month = item.Month
                });
            }
            return new MonthIndexesApi()
            {
                Id = input.Id,
                MonthIndex = indexesList
            };
        }
        public static IEnumerable<MonthIndexesApi> MapToApi(IEnumerable<MonthIndexes> input)
        {
            foreach (var indexes in input)
            {
                yield return new MonthIndexesApi()
                {
                    Id = indexes.Id,
                    MonthIndex = indexes.MonthIndex.Select(i => new MonthIndexApi
                    {
                        Id = i.Id,
                        SavingsRate = i.SavingsRate,
                        AmountsId = i.AmountsId,
                        ExpencesId = i.ExpencesId,
                        IncomesId = i.IncomesId,
                        Month = i.Month
                    })
                };
            }
        }


        public static MonthIndex MapToModel(MonthIndexApi input)
        {
            return new MonthIndex()
            {
                Id = input.Id,
                SavingsRate = input.SavingsRate,
                AmountsId = input.AmountsId,
                ExpencesId = input.ExpencesId,
                IncomesId = input.IncomesId,
                Month = input.Month
            };
        }

        public static IEnumerable<MonthIndex> MapToModel(IEnumerable<MonthIndexApi> input)
        {
            foreach (var item in input)
            {
                yield return new MonthIndex()
                {
                    Id = item.Id,
                    SavingsRate = item.SavingsRate,
                    AmountsId = item.AmountsId,
                    ExpencesId = item.ExpencesId,
                    IncomesId = item.IncomesId,
                    Month = item.Month
                };
            }
        }

        public static IEnumerable<MonthIndexes> MapToModel(IEnumerable<MonthIndexesApi> input)
        {
            foreach (var indexes in input)
            {
                yield return new MonthIndexes()
                {
                    Id = indexes.Id,
                    MonthIndex = indexes.MonthIndex.Select(i => new MonthIndex
                    {
                        Id = i.Id,
                        SavingsRate = i.SavingsRate,
                        AmountsId = i.AmountsId,
                        ExpencesId = i.ExpencesId,
                        IncomesId = i.IncomesId,
                        Month = i.Month
                    })
                };
            }
        }
    }
}
