using MadExpenceTracker.Core.Interfaces.Services;
using Moq;

namespace MadExpenceTracker.Server.Test
{
    public class IncomeControllerTest
    {
        Mock<IIncomeService> _serviceMock;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IIncomeService>();
        }

        [Test]
        public void CreateIncomeTest()
        {
            Assert.Pass();
        }

        [Test]
        public void UpdateIncomeTest()
        {
            Assert.Pass();
        }

        [Test]
        public void GetIncomeTest()
        {
            Assert.Pass();
        }

        [Test]
        public void GetIncomesTest()
        {
            Assert.Pass();
        }

        [Test]
        public void DeleteIncomeTest()
        {
            Assert.Pass();
        }

        [Test]
        public void GetIncomesByIdTest()
        {
            Assert.Pass();
        }

        [Test]
        public void GetCurrentIncomesTest()
        {
            Assert.Pass();
        }
    }
}
