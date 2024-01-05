using MadExpenceTracker.Core.Interfaces.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Server.Test
{
    public class ConfigurationControllerTest
    {
        Mock<IConfigurationService> _serviceMock;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IConfigurationService>();
        }

        [Test]
        public void GetConfigurationTest()
        {
            Assert.Pass();
        }

        [Test]
        public void UpdateConfigurationTest()
        {
            Assert.Pass();
        }
    }
}
