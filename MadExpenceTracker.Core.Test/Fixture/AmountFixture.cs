using MadExpenceTracker.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Core.Test.Fixture
{
    public static class AmountFixture
    {
        public static Amount GetAmount()
        {
            return new Amount
            {
                Id = Guid.NewGuid(),
                Savings = 200000,
                TotalAditionalExpences = 20,
                TotalBaseExpences = 100,
                TotalIncomes = 1000000
            };
        }

        public static Amounts GetAmounts()
        {
            return new Amounts
            {
                Id = Guid.NewGuid(),
                RunningMonth = "2023/12",
                Amount = new List<Amount>
                {
                    new Amount
                    {
                        Savings = 200000,
                    TotalAditionalExpences = 20,
                    TotalBaseExpences = 100,
                    TotalIncomes = 1000000
                    }
                }
            };
        }
    }
}
