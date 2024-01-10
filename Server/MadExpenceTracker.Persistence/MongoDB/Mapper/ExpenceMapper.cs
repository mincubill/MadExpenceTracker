using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Persistence.MongoDB.Model;

namespace MadExpenceTracker.Persistence.MongoDB.Mapper
{
    internal static class ExpenceMapper
    {
        public static Expences MapToModel(ExpencesMongo input)
        {
            List<Expence> expencesList = new List<Expence>();
            if (input.Expences != null && input.Expences.Any())
            {
                foreach (var item in input.Expences)
                {
                    expencesList.Add(new Expence
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Amount = item.Amount,
                        Date = item.Date,
                        ExpenceType = Enum.Parse<ExpenceType>(item.ExpenceType)
                    });
                }
            }
                
            return new Expences()
            {
                Id = input.Id,
                RunningMonth = input.RunningMonth ?? string.Empty,
                Expence = expencesList
            };
        }

        public static Expence MapToModel(ExpenceMongo input)
        {
            return new Expence()
            {
                Id = input.Id,
                Name = input.Name,
                Amount = input.Amount,
                Date = input.Date,
                ExpenceType = Enum.Parse<ExpenceType>(input.ExpenceType)
            };
        }

        public static IEnumerable<Expences> MapToModel(IEnumerable<ExpencesMongo> input)
        {
            foreach (var expences in input)
            {
                yield return new Expences()
                {
                    Id = expences.Id,
                    RunningMonth = expences.RunningMonth,
                    Expence = expences.Expences != null ? expences.Expences.Select( e => new Expence
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Amount = e.Amount,
                        Date = e.Date,
                        ExpenceType = Enum.Parse<ExpenceType>(e.ExpenceType)
                    }) : new List<Expence>()
                };
            }
        }

        public static ExpenceMongo MapToMongo(Expence input)
        {
            return new ExpenceMongo()
            {
                Id = input.Id,
                Name = input.Name,
                Amount = input.Amount,
                Date = input.Date,
                ExpenceType = input.ExpenceType.ToString()
            };
        }
    }
}
