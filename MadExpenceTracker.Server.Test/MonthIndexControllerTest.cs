using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Interfaces.UseCase;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Server.Test
{
    public class MonthIndexControllerTest
    {
        Mock<IMonthIndexingService> _serviceMock;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IMonthIndexingService>();
        }

        [Test]
        public void GetMonthIndexTest()
        {
            Assert.Pass();
        }

        [Test]
        public void SaveMonthIndexTest()
        {
            Assert.Pass();
        }
    }
}
