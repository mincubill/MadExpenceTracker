using MadExpenceTracker.Core.Model;

namespace MadExpenceTracker.Core.Test.Fixture;

public static class MonthIndexFixture
{
    public static MonthIndexes GetMonthIndexes()
    {
        return new MonthIndexes()
        {
            Id = new Guid(),
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
}