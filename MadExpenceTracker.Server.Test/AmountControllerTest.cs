using MadExpenceTracker.Core.Interfaces.Services;
using Moq;

namespace MadExpenceTracker.Server.Test
{
    public class Tests
    {
        Mock<IAmountsService> _serviceMock;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IAmountsService>();
        }

        [Test]
        public void GetAmountByITest()
        {
            Assert.Pass();
        }

        [Test]
        public void CalculatedAmountTest()
        {
            Assert.Pass();
        }

        [Test]
        public void GetAmountTest()
        {
            Assert.Pass();
        }

        [Test]
        public void SaveAmountsTest()
        {
            Assert.Pass();
        }

        [Test]
        public void GetAmountsTest()
        {
            Assert.Pass();
        }
    }
}