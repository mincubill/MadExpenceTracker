using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Core.Services;
using MadExpenceTracker.Core.Test.Fixture;
using Moq;

namespace MadExpenceTracker.Core.Test
{
    public class ExpencesServiceTest
    {
        private IExpencesService _service;
        private Mock<IExpencePersistence> _persistence;

        [SetUp]
        public void Setup()
        {
            _persistence = new Mock<IExpencePersistence>();
            _service = new ExpencesService(_persistence.Object);
        }

        [Test]
        public void GetAllTest()
        {
            IEnumerable<Expences> expences = new List<Expences>() { ExpencesFixture.GetExpences() };

            _persistence.Setup(e => e.GetAll()).Returns(expences);

            IEnumerable<Expences> res = _service.GetAll();

            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void GetExpencesByIdTest()
        {
            Expences expences =  ExpencesFixture.GetExpences() ;

            _persistence.Setup(e => e.Get(expences.Id)).Returns(expences);

            Expences res = _service.GetExpences(expences.Id);

            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void GetExpencesByIsActiveTest()
        {
            Expences expences = ExpencesFixture.GetExpences();

            _persistence.Setup(e => e.GetByActive(true)).Returns(expences);

            Expences res = _service.GetExpences(true);

            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void GetExpenceTest()
        {
            Expence expence = ExpencesFixture.GetExpence();

            _persistence.Setup(e => e.GetExpence(expence.Id)).Returns(expence);

            Expence res = _service.GetExpence(expence.Id);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Name, Is.EqualTo("test2"));
        }

        [Test]
        public void CreateTest()
        {
            Expence expence = ExpencesFixture.GetExpence();
            Expences expences = ExpencesFixture.GetExpences();

            _persistence.Setup(e => e.AddExpence(expence)).Returns(expences);

            Expences res = _service.Create(expence);

            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void CreateExpenceWithoutIdTest()
        {
            Expence expence = ExpencesFixture.GetExpence();
            expence.Id = Guid.Empty;
            Expences expences = ExpencesFixture.GetExpences();

            _persistence.Setup(e => e.AddExpence(expence)).Returns(expences);

            Expences res = _service.Create(expence);

            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void CreateNewMonthTest()
        {
            _persistence.Setup(e => e.CreateNewExpencesDocument($"{DateTime.Now.Year}/{DateTime.Now.Month}")).Returns(true);

            bool res = _service.CreateNewMonth();

            Assert.That(res, Is.True);
        }

        [Test]
        public void UpdateTest()
        {
            string expected = "updateao";
            Expence expence = ExpencesFixture.GetExpence();
            expence.Name = "updateao";
            _persistence.Setup(e => e.Update(expence)).Returns(true);

            Expence res = _service.Update(expence);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Name, Is.EqualTo(expected));
        }

        [Test]
        public void UpdateFail()
        {
            string expected = "updateao";
            Expence expence = ExpencesFixture.GetExpence();
            expence.Name = "updateao";
            _persistence.Setup(e => e.Update(expence)).Returns(false);

            Assert.Throws<Exception>(() => _service.Update(expence));
        }

        [Test]
        public void DeleteTest()
        {
            bool expected = true;
            Expence expence = ExpencesFixture.GetExpence();
            _persistence.Setup(e => e.Delete(expence.Id)).Returns(true);

            bool res = _service.Delete(expence.Id);

            Assert.That(res, Is.EqualTo(expected));
        }

        [Test]
        public void CloseMonthTest()
        {
            bool expected = true;
            Expence expence = ExpencesFixture.GetExpence();
            _persistence.Setup(e => e.UpdateExpencesIsActive(false, "2023/12")).Returns(true);

            bool res = _service.CloseMonth("2023/12");

            Assert.That(res, Is.EqualTo(expected));
        }
    }
}
