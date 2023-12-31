using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Core.Services;
using MadExpenceTracker.Core.Test.Fixture;
using MadExpenceTracker.Persistence.MongoDB.MongoConfiguration;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Core.Test
{
    public class AmountsServiceTest
    {
        private Mock<IAmountsPersistence> _amountPersistence;
        private Mock<IExpencePersistence> _expencesPersistence;
        private Mock<IIncomePersistence> _incomePersistence;
        private Mock<IConfigurationPersistence> _configurationPersistence;

        private IAmountsService _amountsService;

        [SetUp]
        public void Setup()
        {
            _amountPersistence = new Mock<IAmountsPersistence>();
            _expencesPersistence = new Mock<IExpencePersistence>();
            _incomePersistence = new Mock<IIncomePersistence>();
            _configurationPersistence = new Mock<IConfigurationPersistence>();

            _amountsService = new AmountsService(
                _amountPersistence.Object,
                _expencesPersistence.Object,
                _incomePersistence.Object,
                _configurationPersistence.Object);
        }

        [Test]
        public void CalculatedGetAmountTest()
        {
            Guid expencesId = ExpencesFixture.GetExpences().Id;
            Guid incomesId = IncomesFixture.GetIncomes().Id;

            _expencesPersistence.Setup(a => a.Get(expencesId)).Returns(ExpencesFixture.GetExpences());
            _incomePersistence.Setup(i => i.Get(incomesId)).Returns(IncomesFixture.GetIncomes());
            _configurationPersistence.Setup(c => c.GetConfiguration()).Returns(ConfigurationFixture.GetConfiguration());

            Amount res = _amountsService.GetAmount(expencesId, incomesId);

            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void GetAmountsTest()
        {
            var amounts = new List<Amounts>() { AmountFixture.GetAmounts() }.AsEnumerable();
            
            _amountPersistence.Setup(a => a.GetAmounts()).Returns(amounts);

            Amounts res = _amountsService.GetAmounts();

            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void GetAmountTest()
        {
            var amount = AmountFixture.GetAmounts().Amount.First();
            var amounts = AmountFixture.GetAmounts();

            _amountPersistence.Setup(a => a.GetAmounts(amount.Id)).Returns(amounts);

            Amount res = _amountsService.GetAmount(amount.Id);
            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void CreateAmountTest()
        {
            var amount = AmountFixture.GetAmount();
            var amounts = AmountFixture.GetAmounts();

            _amountPersistence.Setup(a => a.AddAmount(amount)).Returns(amounts);

            Amounts res = _amountsService.Create(amount);
            Assert.That(res, Is.Not.Null);
        }
    }
}
