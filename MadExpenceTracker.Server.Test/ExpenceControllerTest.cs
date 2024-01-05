using MadExpenceTracker.Core.Interfaces.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Server.Test
{
    public class ExpenceControllerTest
    {
        Mock<IExpencesService> _serviceMock;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IExpencesService>();
        }

        [Test]
        public void CreateExpenceTest()
        {
            Assert.Pass();
        }

        [Test]
        public void UpdateExpenceTest()
        {
            Assert.Pass();
        }

        [Test]
        public void GetExpenceTest()
        {
            Assert.Pass();
        }

        [Test]
        public void DeleteExpenceTest()
        {
            Assert.Pass();
        }

        [Test]
        public void GetExpencesTest()
        {
            Assert.Pass();
        }

        [Test]
        public void GetExpencesByIdTest()
        {
            Assert.Pass();
        }

        [Test]
        public void GetCurrentExpencesTest()
        {
            Assert.Pass();
        }
    }
}
