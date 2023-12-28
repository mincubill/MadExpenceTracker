using MadExpenceTracker.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Core.Test.Fixture
{
    public static class ExpencesFixture
    {
        public static Expences GetExpences()
        {
            return new Expences
            {
                Id = Guid.Parse("fd76aa75-1628-4fd6-960a-64d62febbd9f"),
                RunningMonth = "2023/12",
                Expence = new List<Expence>
                {
                    new Expence
                    {
                        Id = Guid.Parse("e96157a0-f966-4f86-832f-d394de7f75e2"),
                        Amount = 100,
                        Date = DateTime.Now.AddDays(-8),
                        ExpenceType = ExpenceType.Base,
                        Name = "test1"
                    },
                    new Expence
                    {
                        Id = Guid.Parse("329aa332-c2ce-4f96-af38-140c6f056b99"),
                        Amount = 200,
                        Date = DateTime.Now.AddDays(-2),
                        ExpenceType = ExpenceType.Aditional,
                        Name = "test2"
                    },
                }
            };
        }

        public static Expence GetExpence()
        {

            return new Expence
            {
                Id = Guid.Parse("329aa332-c2ce-4f96-af38-140c6f056b99"),
                Amount = 200,
                Date = DateTime.Now.AddDays(-2),
                ExpenceType = ExpenceType.Aditional,
                Name = "test2"
            };
        }
    }
}
