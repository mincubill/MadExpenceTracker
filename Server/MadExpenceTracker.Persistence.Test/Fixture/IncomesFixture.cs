using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Persistence.MongoDB.Model;


namespace MadExpenceTracker.Persistence.Test.Fixture
{
    public static class IncomesFixture
    {
        public static Incomes GetIncomes()
        {
            return new Incomes
            {
                Id = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac942e0"),
                RunningMonth = "2023/12",
                Income = new List<Income>
                {
                    new Income
                    {
                        Id = Guid.Parse("69cf8cbf-6ef1-4e0a-856a-056d95d7977d"),
                        Name = "Sueldo",
                        Date = DateTime.Parse("2023/11/01"),
                        Amount = 1000000,
                    }
                }
            };
        }

        public static Income GetIncome()
        {
            return new Income
            {
                Id = Guid.Parse("69cf8cbf-6ef1-4e0a-856a-056d95d7977d"),
                Name = "Sueldo",
                Date = DateTime.Parse("2023/11/01"),
                Amount = 1000000,
            };
        }
        
        public static IncomesMongo GetIncomesMongo()
        {
            return new IncomesMongo
            {
                Id = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac942e0"),
                RunningMonth = "2023/12",
                Incomes = new List<IncomeMongo>
                {
                    new IncomeMongo
                    {
                        Id = Guid.Parse("69cf8cbf-6ef1-4e0a-856a-056d95d7977d"),
                        Name = "Sueldo",
                        Date = DateTime.Parse("2023/11/01"),
                        Amount = 1000000,
                    }
                }
            };
        }

        public static IncomeMongo GetIncomeMongo()
        {
            return new IncomeMongo
            {
                Id = Guid.Parse("69cf8cbf-6ef1-4e0a-856a-056d95d7977d"),
                Name = "Sueldo",
                Date = DateTime.Parse("2023/11/01"),
                Amount = 1000000,
            };
        }
    }
}
