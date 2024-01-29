using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Persistence.MongoDB.Model;

namespace MadExpenceTracker.Persistence.Test.Fixture;

public static class MonthIndexFixture
{
    public static MonthIndexes GetMonthIndexes()
    {
        return new MonthIndexes()
        {
            Id = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac00000"),
            MonthIndex = new List<MonthIndex>()
            {
                new MonthIndex()
                {
                    Id = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac05489"),
                    Month = "2023/12",
                    AmountsId = Guid.Parse("9c64e4d9-c9af-4714-8650-c98e6ebecfa7"),
                    ExpencesId = Guid.Parse("fd76aa75-1628-4fd6-960a-64d62febbd9f"),
                    IncomesId = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac942e0"),
                    SavingsRate = 20
                }
            }
        };
    }

    public static MonthIndex GetMonthIndex()
    {
        return new MonthIndex()
        {
            Id = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac05489"),
            Month = "2023/12",
            AmountsId = Guid.Parse("9c64e4d9-c9af-4714-8650-c98e6ebecfa7"),
            ExpencesId = Guid.Parse("fd76aa75-1628-4fd6-960a-64d62febbd9f"),
            IncomesId = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac942e0"),
            SavingsRate = 20
        };
    }
    
    public static MonthIndexesMongo GetMonthIndexesMongo()
    {
        return new MonthIndexesMongo()
        {
            Id = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac00000"),
            MonthIndex = new List<MonthIndexMongo>()
            {
                new MonthIndexMongo()
                {
                    Id = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac05489"),
                    Month = "2023/12",
                    AmountsId = Guid.Parse("9c64e4d9-c9af-4714-8650-c98e6ebecfa7"),
                    ExpencesId = Guid.Parse("fd76aa75-1628-4fd6-960a-64d62febbd9f"),
                    IncomesId = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac942e0"),
                    SavingsRate = 20,
                    AditionalExpencesRate = 30,
                    BaseExpencesRate = 50,
                },
                new MonthIndexMongo()
                {
                    Id = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cacaaaaa"),
                    Month = "2023/12",
                    AmountsId = Guid.Parse("9c64e4d9-c9af-4714-8650-c98e6ebecfa7"),
                    ExpencesId = Guid.Parse("fd76aa75-1628-4fd6-960a-64d62febbd9f"),
                    IncomesId = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac942e0"),
                    SavingsRate = 20,
                    AditionalExpencesRate = 30,
                    BaseExpencesRate = 50,
                }
            },

        };
    }

    public static MonthIndexMongo GetMonthIndexMongo()
    {
        return new MonthIndexMongo()
        {
            Id = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac05489"),
            Month = "2023/12",
            AmountsId = Guid.Parse("9c64e4d9-c9af-4714-8650-c98e6ebecfa7"),
            ExpencesId = Guid.Parse("fd76aa75-1628-4fd6-960a-64d62febbd9f"),
            IncomesId = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac942e0"),
            SavingsRate = 20,
            AditionalExpencesRate = 30,
            BaseExpencesRate = 50,
        };
    }
}