using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Server.Model;

namespace MadExpenceTracker.Server.Mapper
{
    public static class ExpencesMapper
    {
        public static ExpencesApi MapToApi(Expences input)
        {
            List<ExpenceApi> expencesList = new List<ExpenceApi>();
            foreach (var item in input.Expence)
            {
                expencesList.Add(new ExpenceApi
                {
                    Id = item.Id,
                    Name = item.Name,
                    Amount = item.Amount,
                    Date = item.Date,
                    ExpenceType = (Model.ExpenceType)item.ExpenceType,
                });
            }
            return new ExpencesApi()
            {
                Id = input.Id,
                RunningMonth = input.RunningMonth,
                Expence = expencesList
            };
        }

        public static ExpenceApi MapToApi(Expence input)
        {
            return new ExpenceApi()
            {
                Id = input.Id,
                Name = input.Name,
                Amount = input.Amount,
                Date = input.Date,
                ExpenceType = (Model.ExpenceType)input.ExpenceType
            };
        }

        public static IEnumerable<ExpencesApi> MapToApi(IEnumerable<Expences> input)
        {
            foreach (var expences in input)
            {
                yield return new ExpencesApi()
                {
                    Id = expences.Id,
                    RunningMonth = expences.RunningMonth,
                    Expence = expences.Expence.Select(e => new ExpenceApi
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Amount = e.Amount,
                        Date = e.Date,
                        ExpenceType = (Model.ExpenceType)e.ExpenceType,
                    })
                };
            }
        }

        public static Expence MapToModel(ExpenceApi input)
        {
            return new Expence()
            {
                Id = input.Id,
                Name = input.Name,
                Amount = input.Amount,
                Date = input.Date,
                ExpenceType = (Core.Model.ExpenceType)(input.ExpenceType),
            };
        }

        public static Expences MapToModel(ExpencesApi input)
        {
            return new Expences()
            {
                Id = input.Id,
                RunningMonth = input.RunningMonth,
                Expence = input.Expence.Select(e => new Expence
                {
                    Id = e.Id,
                    Name = e.Name,
                    Amount = e.Amount,
                    Date = e.Date,
                    ExpenceType = (Core.Model.ExpenceType)e.ExpenceType,
                })
            };
        }

        public static IEnumerable<Expence> MapToModel(IEnumerable<ExpenceApi> input)
        {
            foreach (var item in input)
            {
                yield return new Expence()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Amount = item.Amount,
                    Date = item.Date,
                    ExpenceType = (Core.Model.ExpenceType) item.ExpenceType,
                };
            }
        }

        public static IEnumerable<Expences> MapToModel(IEnumerable<ExpencesApi> input)
        {
            foreach (var expences in input)
            {
                yield return new Expences()
                {
                    Id = expences.Id,
                    RunningMonth = expences.RunningMonth,
                    Expence = expences.Expence.Select(e => new Expence
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Amount = e.Amount,
                        Date = e.Date,
                        ExpenceType = (Core.Model.ExpenceType) e.ExpenceType,
                    })
                };
            }
        }
    }
}
